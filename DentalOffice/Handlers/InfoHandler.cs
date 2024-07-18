using DentalOffice.Areas.Identity.Data;
using DentalOffice.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DentalOffice.Handlers;

public interface IInfoHandler
{
    
    public Task<List<DoctorDto>> GetDoctors();
    public Task<List<UserDto>> GetPatients();
    public Task<DoctorDto> GetDoctorById(Guid id);
    public Task<UserDto> GetPatientById(Guid id);
    
}


public class InfoHandler : IInfoHandler
{
    private readonly ApplicationDbContext _context;
    private readonly AuthDbContext _auth;
    
    public InfoHandler(ApplicationDbContext context, AuthDbContext auth)
    {
        _context = context;
        _auth = auth;
    }

    public async Task<List<DoctorDto>> GetDoctors()
    {
        var doctors = await _context.Doctors.ToListAsync();
        return doctors.Select(d => new DoctorDto
        {
            Name = d.Name,
            UserId = d.UserId
        }).ToList();
    }

    public async Task<List<UserDto>> GetPatients()
    {
        var patients = await _auth.Users.ToListAsync();
        return patients.Select(p => new UserDto
        {
            Id = Guid.Parse(p.Id),
            Email = p.Email,
            FirstName = p.FirstName,
            LastName = p.LastName,
            PhoneNumber = p.PhoneNumber
        }).ToList();
    }

    public async Task<DoctorDto> GetDoctorById(Guid id)
    {
        var doctor = await _context.Doctors.FindAsync(id);
        return new DoctorDto
        {
            Name = doctor.Name,
            UserId = doctor.UserId
        };
    }

    public async Task<UserDto> GetPatientById(Guid id)
    {
        var patient = await _auth.Users.FindAsync(id.ToString());
        return new UserDto
        {
            Id = Guid.Parse(patient.Id),
            Email = patient.Email,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            PhoneNumber = patient.PhoneNumber
        };
    }


}