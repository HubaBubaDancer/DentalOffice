using DentalOffice.Dtos;
using DentalOffice.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DentalOffice.Handlers;

public interface IAppointmentHandler
{
    
    public Task CreateAppointment(AppointmentDto appointment);
    public Task UpdateAppointment(AppointmentDto appointment, Guid id);
    public Task<List<Appointment>> GetAppointments();
    public Task<List<Appointment>> GetAppointmentsByDoctorId(Guid doctorId);
    public Task<List<DateTime>> GetReservedDates();
    public Task DeleteAppointment(Guid id);
}


public class AppointmentHandler : IAppointmentHandler
{
        
        private readonly ApplicationDbContext _context;
    
        public AppointmentHandler(ApplicationDbContext context)
        {
            _context = context;
        }
    
        public async Task CreateAppointment(AppointmentDto appointmentDto)
        {
            var appointment = new Appointment
            {
                Date = appointmentDto.Date,
                DoctorId = appointmentDto.DoctorId,
                PatientId = appointmentDto.PatientId,
                ProcedureId = appointmentDto.ProcedureId
            };

            var reserved = _context.Appointments
                .Where(a => a.Date == appointmentDto.Date).ToList();

            foreach (var res in reserved)
            {
                if (res.DoctorId == appointmentDto.DoctorId || res.PatientId == appointmentDto.PatientId)
                {
                    return;
                }
            }
            
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
        }
    
        public async Task UpdateAppointment(AppointmentDto appointmentDto, Guid id)
        {
            var appointment = _context.Appointments.FirstOrDefault(p => p.Id == id);
            
            appointment.Date = appointmentDto.Date;
            appointment.DoctorId = appointmentDto.DoctorId;
            appointment.PatientId = appointmentDto.PatientId;
            appointment.ProcedureId = appointmentDto.ProcedureId;
            
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
        }
        
        public async Task<List<Appointment>> GetAppointments()
        {
            return await _context.Appointments.ToListAsync();
        }
        
        public async Task<List<Appointment>> GetAppointmentsByDoctorId(Guid doctorId)
        {
            return await _context.Appointments.Where(a => a.DoctorId == doctorId).ToListAsync();
        }
        
        public async Task<List<DateTime>> GetReservedDates()
        {
            var reservedDates = await _context.Appointments.Select(a => a.Date).ToListAsync();
            return reservedDates;
        }
        
        public async Task DeleteAppointment(Guid id)
        {
            var appointment = _context.Appointments.FirstOrDefault(p => p.Id == id);
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
        }
        
        
}