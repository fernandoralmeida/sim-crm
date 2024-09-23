using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sim.Application.Interfaces;
using Sim.UI.Web.Functions;

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

    [HttpGet("empresa/{cnpj?}")]
    public async Task<IActionResult> GetEmpresa([FromRoute] string? cnpj)
    {
        try
        {
            // Validação básica do CNPJ
            if (string.IsNullOrWhiteSpace(cnpj))
            {
                return BadRequest(new { message = "CNPJ não pode ser vazio." });
            }

            // Tente obter a lista de empresas com o CNPJ fornecido
            var _p = cnpj.MaskRemove().Mask("##.###.###/####-##");
            var emp = await _empresa.DoListAsync(s => s.CNPJ == _p);

            if (emp == null || !emp.Any())
            {
                return NotFound(new { message = "Empresa não encontrada com o CNPJ fornecido." });
            }

            return Ok(emp);
        }
        catch (Exception ex)
        {
            // Log do erro para fins de auditoria e depuração
            // _logger.LogError(ex, "Erro ao obter empresa com CPF {cnpj}", cnpj);

            // Retornar uma resposta de erro com status 500
            var errorResponse = new
            {
                message = "Ocorreu um erro ao processar sua solicitação.",
                detailedMessage = ex.Message // Detalhes do erro para depuração
            };

            return StatusCode(500, errorResponse);
        }
    }

}