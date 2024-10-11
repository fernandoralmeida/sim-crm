using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;
using Sim.Identity.Entity;
using Sim.Identity.Interfaces;

namespace Sim.UI.Web.Areas.Identity.Pages;

[Authorize]
public class ThemeModel : PageModel
{
    private readonly UserManager<ApplicationUser> _usermanager;
    private readonly IServiceUser _userservice;
    public ThemeModel(UserManager<ApplicationUser> usermanager,
                        IServiceUser userservice)
    {
        _usermanager = usermanager;
        _userservice = userservice;
    }

    [TempData]
    public string? StatusMessage { get; set; }

    public async Task OnGetAsync(string theme, string url)
    {
        string returnURL = url.Replace("%2F", "/");
        returnURL = returnURL.Remove(0, 1);
        try
        {
            var _user = await _usermanager.GetUserAsync(User);
            HttpContext.Session.SetString("Theme", theme ?? "light");
            await _userservice.SetThemeAsync(_user.Id, theme!);
            Response.Redirect($"/{returnURL}");
        }
        catch (Exception ex)
        {
            StatusMessage = $"Erro: {ex.Message}";
            Response.Redirect($"/{returnURL}");
        }
    }
}