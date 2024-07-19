using DentalOffice.Areas.Identity.Data;
using DentalOffice.Dtos;
using DentalOffice.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.DotNet.Scaffolding.Shared.Project;

namespace DentalOffice.Handlers;

public interface IRegistrationHandler
{
    public Task RegisterDoctor(DoctorDto doctor);
    public Task RegisterUser(RegisterModel model);
    
}

public class RegistrationHandler : IRegistrationHandler
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public RegistrationHandler(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task RegisterUser(RegisterModel model)
    {
        var user = new ApplicationUser
        {
            UserName = model.FirstName,
            Email = model.Email,
            PhoneNumber = model.Phone,
            FirstName = model.FirstName,
            LastName = model.LastName
        };
        
        var result = await _userManager.CreateAsync(user, model.Password);
        
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "User");
        }
    }
    
    public async Task RegisterDoctor(DoctorDto doctorDto)
    {
        var user = await _userManager.FindByIdAsync(doctorDto.UserId.ToString());

        if (user == null)
        {
            return;
        }

        await _userManager.AddToRolesAsync(user, new[] {"Doctor"});
        
        var doctor = new Doctor
        {
            Name = doctorDto.Name,
            UserId = doctorDto.UserId
        };
        
        await _context.Doctors.AddAsync(doctor);
        await _context.SaveChangesAsync();
    }
}