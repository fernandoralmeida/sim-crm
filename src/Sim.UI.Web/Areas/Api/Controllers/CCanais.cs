using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sim.Application.Interfaces;

namespace Sim.UI.Web.Areas.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/v1")]
public class CCanais : ControllerBase
{
    private readonly IAppServiceCanal _canal;
    public CCanais(IAppServiceCanal canal)
    {
        _canal = canal;
    }

    [HttpGet("canais/{setor}")]
    public async Task<IActionResult> GetCanais([FromRoute] string setor)
        => Ok(await _canal.DoListJson(setor));

    
}