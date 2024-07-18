using DentalOffice.Models;

namespace DentalOffice.Dtos;

public class AppointmentDto
{
    public Guid PatientId { get; set; }
    public Guid DoctorId { get; set; }
    public DateTime Date { get; set; }
    public Guid ProcedureId { get; set; }
}