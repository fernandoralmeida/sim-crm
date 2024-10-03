using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Sim.UI.Web.Areas.Admin.ViewModel;
using Sim.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;
using Sim.Identity.Entity;
using Sim.Identity.Config;

namespace Sim.UI.Web.Areas.Admin.Pages.Manager
{

    [Authorize(Policy = "AdminOrAccounts")]
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

        public IEnumerable<ApplicationUser>? Users_Admin_Global { get; set; }
        public IEnumerable<ApplicationUser>? Users_Admin_Account { get; set; }
        public IEnumerable<ApplicationUser>? Users_Admin_Config { get; set; }

        private async Task LoadAsync()
        {           
            var _adm_global = await _userManager.GetUsersInRoleAsync(AccountType.Adm_Global); 
            var _adm_account = await _userManager.GetUsersInRoleAsync(AccountType.Adm_Account); 
            var _adm_config = await _userManager.GetUsersInRoleAsync(AccountType.Adm_Settings); 

            Users_Admin_Global = _adm_global.Where(s => s.LockoutEnabled == true).OrderBy(o => o.UserName);
            Users_Admin_Account = _adm_account.Where(s => s.LockoutEnabled == true).OrderBy(o => o.UserName);
            Users_Admin_Config = _adm_config.Where(s => s.LockoutEnabled == true).OrderBy(o => o.UserName);

            var _lockout_off = await _appIdentity.ListAllAsync();
            var _users = _lockout_off.Where(s => s.LockoutEnabled == true).ToList();

            foreach (var u in _lockout_off) {
                foreach (var g in Users_Admin_Global) {
                    if(g.UserName == u.UserName)
                        _users.Remove(u);
                }
                foreach (var g in Users_Admin_Account) {
                    if(g.UserName == u.UserName)
                        _users.Remove(u);
                }
                foreach (var g in Users_Admin_Config) {
                    if(g.UserName == u.UserName)
                        _users.Remove(u);
                }
            }

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
