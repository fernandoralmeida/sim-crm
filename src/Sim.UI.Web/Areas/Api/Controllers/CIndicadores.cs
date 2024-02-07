using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;
using Sim.Application.Indicadores.Interfaces;

namespace Sim.UI.Web.Areas.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/v1")]
public class CIndicadores : ControllerBase
{
    private readonly IAppServiceAtendimento _atendimentos;
    private readonly IAppIndicadores _indicadores;
    private readonly IAppServiceEvento _eventos;

    public CIndicadores(IAppServiceAtendimento atendimento,
        IAppIndicadores indicadores,
        IAppServiceEvento eventos)
    {
        _atendimentos = atendimento;
        _indicadores = indicadores;
        _eventos = eventos;
    }

    [HttpGet("indicadores/antendimentos/{ano?}/{setor?}")]
    public async Task<IActionResult> DoAtendimentos([FromRoute] int ano, [FromRoute] string setor)
     => Ok(setor == null ?
            await _indicadores.DoAtendimentosAsync(s => s.Data!.Value.Year == ano) :
            await _indicadores.DoAtendimentosAsync(s => s.Data!.Value.Year == ano && s.Setor == setor));

}