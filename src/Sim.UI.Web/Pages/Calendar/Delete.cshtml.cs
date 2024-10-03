using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Agenda.Interfaces;

namespace Sim.UI.Web.Pages.Calendar;

[Authorize]
public class DeleteModel : PageModel
{
    private readonly IAppServiceReminder _appServiceReminder;
    public DeleteModel(IAppServiceReminder appServiceReminder)
    {
        _appServiceReminder = appServiceReminder;
    }

    [TempData]
    public string? StatusMessage { get; set; }

    public async Task<IActionResult> OnGetAsync(string id)
    {
        try
        {
            var _reminds = await _appServiceReminder.GetAsNoTrackingAsync(new Guid(id));
            await _appServiceReminder.RemoveAsync(_reminds!);
            return RedirectToPage("/Calendar/Index");
        }
        catch (Exception ex)
        {
            StatusMessage = $"Erro: {ex.Message}";
            return RedirectToPage("/Error");
        }
    }
}