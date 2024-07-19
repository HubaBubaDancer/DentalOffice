using DentalOffice.Dtos;
using DentalOffice.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalOffice.Controllers;

[Authorize]
[Route("api/[controller]")]
public class ClientController : Controller
{
    
    private readonly IProcedureHandler _procedureHandler;
    private readonly IAppointmentHandler _appointmentHandler;
    private readonly IInfoHandler _infoHandler;

    public ClientController(IProcedureHandler procedureHandler, IAppointmentHandler appointmentHandler, IInfoHandler infoHandler)
    {
        _procedureHandler = procedureHandler;
        _appointmentHandler = appointmentHandler;
        _infoHandler = infoHandler;
    }

    [AllowAnonymous]
    [HttpGet("get-non-archived-procedures")]
    public async Task<IActionResult> GetNonArchivedProcedures()
    {
        return Ok(await _procedureHandler.GetNonArchivedProcedures());
    }

    [HttpPost("create-appointment")]
    public async Task<IActionResult> CreateAppointment([FromBody] AppointmentDto appointment)
    {
        await _appointmentHandler.CreateAppointment(appointment);
        return Ok();
    }
    
    [HttpGet("get-reserved-dates")]
    public async Task<IActionResult> GetReservedDates()
    {
        return Ok(await _appointmentHandler.GetReservedDates());
    }
    
    [HttpGet("get-doctors")]
    public async Task<IActionResult> GetDoctors()
    {
        return Ok(await _infoHandler.GetDoctors());
    }

    [HttpGet("get-doctor-by-id/{id}")]
    public async Task<IActionResult> GetDoctorById(Guid id)
    {
        return Ok(await _infoHandler.GetDoctorById(id));
    }
    
    
    
}