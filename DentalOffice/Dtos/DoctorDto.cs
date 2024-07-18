namespace DentalOffice.Dtos;

public class DoctorDto
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public List<ProcedureDto> Procedures { get; set; } = new List<ProcedureDto>();
}
