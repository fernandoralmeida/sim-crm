using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Sim.UI.Web.Areas.Admin.ViewModel;
using Sim.Identity.Interfaces;
using Sim.Identity.Entity;
using Microsoft.AspNetCore.Identity;
using Sim.Identity.Policies;
using NuGet.Protocol;

namespace Sim.UI.Web.Areas.Admin.Pages.Manager
{

    [Authorize(Policy = PolicyExtensions.IsAdminAccounts)]
    public class IndexModel : PageModel
    {
        private readonly IServiceUser _appIdentity;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public IndexModel(IServiceUser appServiceUser,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _appIdentity = appServiceUser;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [TempData]
        public string? StatusMessage { get; set; }

        [BindProperty]
        public VMListUsers? Input { get; set; }

        public IEnumerable<AppUserExtended>? Grupos { get; set; }
        public IEnumerable<KeyValuePair<string, IEnumerable<string>>>? Roles { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SelectedRole { get; set; }
        public IEnumerable<KeyValuePair<string, IEnumerable<string>>>? Funcoes { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SelectedFuncao { get; set; }

        private async Task LoadAsync()
        {
            await Task.Run(() =>
            {
                Funcoes = new List<KeyValuePair<string, IEnumerable<string>>>
                {
                    new(PolicyTypes.Permission, PolicyTypes.ToList())
                };

                Roles = new List<KeyValuePair<string, IEnumerable<string>>>
                {
                    new ("SEDEMPi", from _r in _roleManager.Roles select new string( _r.Name))
                };
            });
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadAsync();
            Grupos = await _appIdentity.GetUsersExtendedAsync();
            return Page();
        }

        public async Task<IActionResult> OnGetLockUnlock(string id, bool blk)
        {
            var _status = await _appIdentity.lockUnlockAsync(id, blk);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await LoadAsync();

            Grupos = await _appIdentity.GetUsersGroupedByRolesAndClaimsAsync(
                SelectedRole,
                SelectedFuncao
            );


            return Page();
        }
    }
}
