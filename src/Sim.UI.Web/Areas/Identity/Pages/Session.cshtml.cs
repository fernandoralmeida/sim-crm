
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;

namespace Sim.UI.Web.Areas.Identity.Pages;

[Authorize]
public class SessionModel : PageModel
{
    private readonly IAppServiceSecretaria _appSecretaria;

    public SessionModel(IAppServiceSecretaria appServiceSecretaria)
    {
        _appSecretaria = appServiceSecretaria;
    }

    [TempData]
    public string? StatusMessage { get; set; }

    public async Task<IActionResult> OnGetAsync(string id)
    {
        // var _route = url.Replace("%2F", "/");

        // var _returnUrl = $"{Request.Scheme}://{Request.Host.ToUriComponent()}{_route}";

        try
        {
            var _unidade = await _appSecretaria.GetAsync(Guid.Parse(id));
            HttpContext.Session.SetString("SetorAtivo", _unidade.Acronimo!);
            StatusMessage = $"Setor {_unidade.Acronimo} selecionado com sucesso!";

            if (_unidade.Acronimo!.Contains("Sebrae"))
                return RedirectToPage("/Index", new { area = "Sebrae" });
            // _returnUrl = $"{Request.Scheme}://{Request.Host.ToUriComponent()}/sebrae";


            else if (_unidade.Acronimo!.Contains("Banco do Povo"))
                return RedirectToPage("/Index", new { area = "Bpp" });
            // _returnUrl = $"{Request.Scheme}://{Request.Host.ToUriComponent()}/bpp";

            else
                return RedirectToPage("/Atendimento/Index");

        }
        catch (Exception ex)
        {
            StatusMessage = $"Erro: {ex.Message}";
            return RedirectToPage("/Atendimento/Index");
        }

        //return Redirect(_returnUrl);
    }
}
