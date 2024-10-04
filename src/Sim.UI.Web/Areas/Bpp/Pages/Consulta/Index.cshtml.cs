using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Identity.Policies;
using Sim.UI.Web.Areas.Bpp.Services;

namespace Sim.UI.Web.Areas.Bpp.Pages.Consulta;

[RoleOrClaimAuthorize(Module.Name, PolicyTypes.Permission, PolicyTypes.Adm_Global)]
public class IndexModel : PageModel
{
    [BindProperty]
    public string? Search { get; set;}
    public void OnGet()
    {
    }
}

