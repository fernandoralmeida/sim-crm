using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sim.Application.Interfaces;

namespace Sim.UI.Web.Areas.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/v1")]
public class CPessoas : ControllerBase
{
    private readonly IAppServicePessoa _pessoa;
    public CPessoas(IAppServicePessoa pessoa)
    {
        _pessoa = pessoa;
    }

    [HttpGet("pessoa/{cpf}")]
    public async Task<IActionResult> GetPessoa([FromRoute]string cpf)
        => Ok(await _pessoa.DoListAsync(s => s.CPF == cpf));


} 