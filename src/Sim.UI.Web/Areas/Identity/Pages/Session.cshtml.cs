
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

    public async Task OnGetAsync(string id, string returnUrl)
    {
        //string returnURL = HttpContext url;

        var _route = returnUrl.Replace("%2F", "/")[1..];

        var _returnUrl = $"{Request.Scheme}://{Request.Host.ToUriComponent()}/{_route}";

        try
        {
            var _unidade = await _appSecretaria.GetAsync(Guid.Parse(id));
            HttpContext.Session.SetString("SetorAtivo", _unidade.Acronimo!);
            StatusMessage = $"Setor {_unidade.Acronimo} selecionado com sucesso!";
            if (_unidade.Acronimo!.Contains("Sebrae"))
                Response.Redirect($"{Request.Scheme}://{Request.Host.ToUriComponent()}/sebrae");
            else if (_unidade.Acronimo!.Contains("Banco do Povo"))
                Response.Redirect($"{Request.Scheme}://{Request.Host.ToUriComponent()}/bpp");
            else
                Response.Redirect(_returnUrl);
        }
        catch (Exception ex)
        {
            StatusMessage = $"Erro: {ex.Message}";
            Response.Redirect(_returnUrl);
        }
    }
}
