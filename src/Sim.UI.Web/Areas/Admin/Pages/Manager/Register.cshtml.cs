using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Sim.UI.Web.Areas.Admin.ViewModel;
using Sim.Identity.Entity;
using Sim.Identity.Config;

namespace Sim.UI.Web.Areas.Admin.Pages.Manager
{

    [Authorize(Policy = "AdminOrAccounts")]
    public class RegisterModel : PageModel
    {

        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [TempData]
        public string? StatusMessage { get; set; }

        [BindProperty]
        public VMRegister? Input { get; set; }


        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                    return Page();
                var newuser = new ApplicationUser()
                {

                    UserName = Input!.UserName,
                    Name = Input.Name,
                    LastName = Input.LastName,
                    Gender = Input.Genero,
                    Email = Input.Email,
                    EmailConfirmed = true
                };

                await _userManager.CreateAsync(newuser, Input.Password);

                return RedirectToPage("./Index");

            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
                return Page();
            }
        }
    }
}
