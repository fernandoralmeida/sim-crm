using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sim.Application.Interfaces;

namespace Sim.UI.Web.Areas.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/v1")]
public class CEmpresas : ControllerBase
{
    private readonly IAppServiceEmpresa _empresa;
    public CEmpresas(IAppServiceEmpresa empresa)
    {
        _empresa = empresa;
    }

    [HttpGet("empresa/{cnpj}")]
    public async Task<IActionResult> GetEmpresa([FromRoute] string cnpj)
        => Ok(await _empresa.DoListAsync(s => s.CNPJ == cnpj));

}