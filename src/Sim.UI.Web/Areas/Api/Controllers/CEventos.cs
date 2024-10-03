using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sim.Application.Agenda.Interfaces;
using Sim.Application.Interfaces;

namespace Sim.UI.Web.Areas.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/v1")]
public class CEventos : ControllerBase
{
    private readonly IAppServiceEvento _eventos;
    public CEventos(IAppServiceEvento eventos)
    {
        _eventos = eventos;
    }

    [HttpGet("eventos/{cd}")]
    public async Task<IActionResult> GetEventos([FromRoute]int cd)
        => Ok(await _eventos.DoListAsync(s => s.Codigo == cd));

} 