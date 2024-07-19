using DentalOffice.Dtos;
using DentalOffice.Handlers;
using DentalOffice.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalOffice.Controllers;

[Authorize(Roles = "Doctor,Admin")]
[Route("api/[controller]")]
public class DoctorController : ControllerBase
{
    private readonly IProcedureHandler _procedureHandler;
    private readonly IAppointmentHandler _appointmentHandler;
    private readonly IInfoHandler _infoHandler;

    public DoctorController(IProcedureHandler procedureHandler, IAppointmentHandler appointmentHandler, IInfoHandler infoHandler)
    {
        _procedureHandler = procedureHandler;
        _appointmentHandler = appointmentHandler;
        _infoHandler = infoHandler;
    }

    [HttpPost("create-procedure")]
    public async Task<IActionResult> CreateProcedure([FromBody] ProcedureDto procedure)
    {
        await _procedureHandler.CreateProcedure(procedure);
        return Ok();
    }
    
    [HttpPost("update-procedure/{id}")]
    public async Task<IActionResult> UpdateProcedure([FromBody] ProcedureDto procedure, Guid id)
    {
        await _procedureHandler.UpdateProcedure(procedure, id);
        return Ok();
    }
    
    [HttpGet("get-procedures")]
    public async Task<List<Procedure>> GetProcedures()
    {
        return await _procedureHandler.GetProcedures();
    }

    [HttpGet("get-all-appointments")]
    public async Task<IActionResult> GetAllAppointments()
    {
        return Ok(await _appointmentHandler.GetAppointments());
    }
    
    [HttpGet("get-appointment-by-doctor-id/{id}")]
    public async Task<IActionResult> GetAppointmentByDoctorId(Guid id)
    {
        return Ok(await _appointmentHandler.GetAppointmentsByDoctorId(id));
    }
    
    [HttpDelete("delete-appointment/{id}")]
    public async Task<IActionResult> DeleteAppointment(Guid id)
    {
        await _appointmentHandler.DeleteAppointment(id);
        return Ok();
    }
    
    [HttpGet("get-patients")]
    public async Task<IActionResult> GetPatients()
    {
        return Ok(await _infoHandler.GetPatients());
    }
    
    [HttpGet("get-patient-by-id/{id}")]
    public async Task<IActionResult> GetPatientById(Guid id)
    {
        return Ok(await _infoHandler.GetPatientById(id));
    }
    
    
    
}