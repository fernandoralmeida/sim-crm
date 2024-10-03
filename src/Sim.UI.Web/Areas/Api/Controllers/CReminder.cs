using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sim.Application.Agenda.Interfaces;

namespace Sim.UI.Web.Areas.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/v1")]
public class CReminder : ControllerBase
{
    private readonly IAppServiceReminder _reminder;
    public CReminder(IAppServiceReminder reminder)
    {
        _reminder = reminder;
    }

    [HttpGet("reminder/{id}")]
    public async Task<IActionResult> GetRiminder([FromRoute]Guid id)
        => Ok(await _reminder.GetAsNoTrackingAsync(id));


} 