namespace DentalOffice.Models;

public class Doctor : BaseDbItem
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public List<Procedure> Procedures { get; set; } = new List<Procedure>();
}