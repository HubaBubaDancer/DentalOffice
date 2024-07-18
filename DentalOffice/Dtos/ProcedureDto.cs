namespace DentalOffice.Dtos;

public class ProcedureDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public bool Archived { get; set; } = false;
}