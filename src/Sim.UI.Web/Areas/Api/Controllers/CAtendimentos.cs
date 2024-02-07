using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sim.Application.Interfaces;

namespace Sim.UI.Web.Areas.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/v1")]
public class CAtendimentos : ControllerBase
{
    private readonly IAppServiceAtendimento _atendimento;
    public CAtendimentos(IAppServiceAtendimento atendimento)
    {
        _atendimento = atendimento;
    }

    [HttpGet("atendimento/{id}")]
    public async Task<IActionResult> GetAtendimento([FromRoute]Guid id)
        => Ok(await _atendimento.GetAsync(id));


} 