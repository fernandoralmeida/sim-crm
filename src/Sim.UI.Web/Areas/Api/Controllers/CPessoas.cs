using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sim.Application.Interfaces;
using Sim.UI.Web.Functions;

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

    [HttpGet("pessoa/{cpf?}")]
    public async Task<IActionResult> GetPessoa([FromRoute] string? cpf)
    {
        try
        {
            // Validação básica do CPF
            if (string.IsNullOrWhiteSpace(cpf))
            {
                return BadRequest(new { message = "CPF não pode ser vazio." });
            }

            // Tente obter a lista de pessoas com o CPF fornecido
            var _p = cpf.MaskRemove().Mask("###.###.###-##");
            var pessoas = await _pessoa.DoListAsync(s => s.CPF == _p);

            if (pessoas == null || !pessoas.Any())
            {
                return NotFound(new { message = "Pessoa não encontrada com o CPF fornecido." });
            }

            return Ok(pessoas);
        }
        catch (Exception ex)
        {
            // Log do erro para fins de auditoria e depuração
            // _logger.LogError(ex, "Erro ao obter pessoa com CPF {cpf}", cpf);

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