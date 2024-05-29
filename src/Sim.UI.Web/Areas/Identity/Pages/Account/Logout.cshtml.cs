using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Identity.Entity;

namespace Sim.UI.Web.Areas.Identity.Pages.Account
{
    [Authorize]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            // Invalidate the session
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync();
            await _signInManager.SignOutAsync();
            
            _logger.LogInformation("Usuário desconectado.");

            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPost(string? returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            //await HttpContext.SignOutAsync();
            _logger.LogInformation("Usuário desconectado.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage("/Index");
            }
        }
    }
}
