namespace DentalOffice.Models;

public class Procedure : BaseDbItem
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public bool Archived { get; set; } = false;
    
}