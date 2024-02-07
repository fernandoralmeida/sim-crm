using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sim.Application.Interfaces;

namespace Sim.UI.Web.Areas.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/v1")]
public class CServicos : ControllerBase
{
    private readonly IAppServiceServico _servicos;
    public CServicos(IAppServiceServico servicos)
    {
        _servicos = servicos;
    }

    [HttpGet("servicos/{setor}")]
    public async Task<IActionResult> GetServicos([FromRoute] string setor)
        => Ok(await _servicos.ToListJson(setor));

    
}