using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Sim.UI.Web.Areas.Admin.ViewModel;
using Sim.Identity.Config;

namespace Sim.UI.Web.Areas.Admin.Pages.Manager
{

    [Authorize(Policy = "AdminOrAccounts")]
    public class ClaimsModel : PageModel
    {
        [TempData]
        public string? StatusMessage { get; set; }

        [BindProperty]
        public VMRoleClaims? Input { get; set; }

        public void OnGet()
        {
        }
    }
}
