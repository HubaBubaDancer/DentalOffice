using System.Security.Claims;
using DentalOffice.Dtos;
using DentalOffice.Models;
using Microsoft.EntityFrameworkCore;

namespace DentalOffice.Handlers;

public interface IProcedureHandler
{
    
    public Task CreateProcedure(ProcedureDto procedure);
    public Task UpdateProcedure(ProcedureDto procedure, Guid id);
    public Task<List<Procedure>> GetProcedures();
    public Task<List<ProcedureDto>> GetNonArchivedProcedures();
}


public class ProceduresHandler : IProcedureHandler
{

    private readonly ApplicationDbContext _context;

    public ProceduresHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateProcedure(ProcedureDto procedureDto)
    {
        var procedure = new Procedure
        {
            Name = procedureDto.Name,
            Price = procedureDto.Price,
            Description = procedureDto.Description,
            Archived = procedureDto.Archived
        };

        await _context.Procedures.AddAsync(procedure);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProcedure(ProcedureDto procedureDto, Guid id)
    {
        var procedure = _context.Procedures.FirstOrDefault(p => p.Id == id);
        
        procedure.Description = procedureDto.Description;
        procedure.Name = procedureDto.Name;
        procedure.Price = procedureDto.Price;
        procedure.Archived = procedureDto.Archived;
        
        _context.Procedures.Update(procedure);
        await _context.SaveChangesAsync();
    }
    
    public async Task<List<Procedure>> GetProcedures()
    {
        return await _context.Procedures.ToListAsync();
    }
    
    public async Task<List<ProcedureDto>> GetNonArchivedProcedures()
    {
        var ans = await _context.Procedures.Where(p => p.Archived == false).ToListAsync();
        return ans.Select(p => new ProcedureDto
        {
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            Archived = p.Archived
        }).ToList();
    }
}