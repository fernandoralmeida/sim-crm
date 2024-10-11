using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Sim.UI.Web.Areas.Admin.ViewModel;
using Sim.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;
using Sim.Identity.Entity;
using Sim.Identity.Policies;

namespace Sim.UI.Web.Areas.Admin.Pages.Manager
{

    [Authorize(Policy = PolicyExtensions.IsAdminAccounts)]
    public class LockoutModel : PageModel
    {
        private readonly IServiceUser _appIdentity;
        private readonly UserManager<ApplicationUser> _userManager;
        public LockoutModel(IServiceUser appServiceUser,
            UserManager<ApplicationUser> userManager)
        {
            _appIdentity = appServiceUser;
            _userManager = userManager;
        }

        [TempData]
        public string? StatusMessage { get; set; }

        [BindProperty]
        public VMListUsers? Input { get; set; }

        private async Task LoadAsync()
        {           
            var _lockout_off = await _appIdentity.ListAllAsync();
            var _users = _lockout_off!.Where(s => s.LockoutEnabled == true).ToList();
            
            Input = new() {
                Users = _users.OrderBy(o => o.UserName)
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadAsync();
            return Page();
        }

        public async Task<IActionResult> OnGetLockUnlock(string id, bool blk) {            
            var _status = await _appIdentity.lockUnlockAsync(id, blk);
            return Page();                       
        }
    }
}
