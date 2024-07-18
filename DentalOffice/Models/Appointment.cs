namespace DentalOffice.Models;

public class Appointment : BaseDbItem
{
    public Guid PatientId { get; set; }
    public Guid DoctorId { get; set; }
    public Guid ProcedureId { get; set; }
    public DateTime Date { get; set; }
}