using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sim.UI.Web.Areas.Admin.ViewModel;
using Sim.Application.Interfaces;
using Sim.Identity.Config;

namespace Sim.UI.Web.Areas.Admin.Pages.Manager
{

    [Authorize(Roles = $"{AccountType.Adm_Global}")]
    public class RolesModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAppServiceSecretaria _appOrg;

        public RolesModel(RoleManager<IdentityRole> roleManager,
            IAppServiceSecretaria appOrg)
        {
            _roleManager = roleManager;
            _appOrg = appOrg;
        }

        [TempData]
        public string? StatusMessage { get; set; }

        [BindProperty]
        public VMRoles? Input { get; set; }
        public SelectList? Funcoes { get; set; }

        [BindProperty]
        public string? Funcao { get; set; }

        private async Task LoadAsync()
        {
            var t = Task.Run(() => _roleManager.Roles.OrderBy(o => o.Name));
            await t;
            Input = new()
            {
                Roles = t.Result.Where(s => s.Name != AccountType.Adm_Global)
            };

            var _list = await _appOrg.DoListAsync();

            var _funcs = new List<string>();
            _funcs = AccountType.ToList().ToList();
            foreach (var l in AccountType.ToList())
                foreach (var sl in Input.Roles.Where(s => s.Name == l))
                    _funcs.Remove(sl.Name);          

            Funcoes = new SelectList(_funcs);
            _ = Input.Roles.OrderBy(o => o.Name);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await LoadAsync();
                if (ModelState.IsValid)
                {
                    var role = new IdentityRole(Input!.Name);
                    var roleresult = await _roleManager.CreateAsync(role);

                    if (roleresult.Succeeded)
                    {
                        Input.Roles = _roleManager.Roles;
                        Input.Name = string.Empty;
                        return Page();
                    }
                    return Page();
                }

                return Page();
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAddFuncAsync()
        {
            try
            {
                await LoadAsync();
                if (ModelState.IsValid)
                {
                    var role = new IdentityRole(Funcao);
                    var roleresult = await _roleManager.CreateAsync(role);

                    if (roleresult.Succeeded)
                    {
                        Input!.Roles = _roleManager.Roles;
                        Funcao = string.Empty;

                        return Page();
                    }
                    return Page();
                }
                
                return Page();
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
                return Page();
            }
        }

        public async Task<IActionResult> OnPostRemoveAsync(string id)
        {
            try
            {
                await LoadAsync();
                if (ModelState.IsValid)
                {
                    var role = await _roleManager.FindByIdAsync(id);

                    if (role == null)
                        return Page();

                    var delete = await _roleManager.DeleteAsync(role);


                    if (!delete.Succeeded)
                    {
                        StatusMessage = delete.Errors.First().ToString();
                        return Page();
                    }
                    return RedirectToPage();

                }
                
                return Page();
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
                return Page();
            }
        }
    }
}
