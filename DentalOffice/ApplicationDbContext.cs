using DentalOffice.Models;
using Microsoft.EntityFrameworkCore;

namespace DentalOffice;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Procedure> Procedures { get; set; }
    


}