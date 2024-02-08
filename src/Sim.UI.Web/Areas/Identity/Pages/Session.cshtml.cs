
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

    public async Task OnGetAsync(string id, string url)
    {
        string returnURL = url.Replace("%2F", "/");
        returnURL = returnURL.Remove(0, 1);
        try
        {
            var _unidade = await _appSecretaria.GetAsync(Guid.Parse(id));
            HttpContext.Session.SetString("SetorAtivo", _unidade.Acronimo!);
            StatusMessage = $"Setor {_unidade.Acronimo} selecionado com sucesso!";
            Response.Redirect($"/{returnURL}");
        }
        catch (Exception ex)
        {
            StatusMessage = $"Erro: {ex.Message}";
            Response.Redirect($"/{returnURL}");
        }
    }
}
