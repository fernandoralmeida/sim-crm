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

    [Authorize(Policy = "AdminOrAccounts")]
    public class RolesModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAppServiceSecretaria _appOrg;
        private readonly IAppServiceSecretaria _secretaria;

        public RolesModel(RoleManager<IdentityRole> roleManager,
            IAppServiceSecretaria appOrg,
            IAppServiceSecretaria secretaria)
        {
            _roleManager = roleManager;
            _appOrg = appOrg;
            _secretaria = secretaria;
        }

        [TempData]
        public string? StatusMessage { get; set; }

        [BindProperty]
        public VMRoles? Input { get; set; }
        public List<KeyValuePair<string, IEnumerable<string>>>? OwnerList { get; set; } = new();

        [BindProperty]
        public string? OwnerSelect { get; set; }

        private async Task LoadAsync()
        {
            var _owners = await _secretaria.DoListHierarquia1Async(await _secretaria.DoListAsync());
            var _subowners = await _secretaria.DoListHierarquia2Async(await _secretaria.DoListAsync());

            var _olist = from _s in _owners.Where(s => s.Hierarquia == Domain.Organizacao.Model.EHierarquia.Secretaria)
                         select (new KeyValuePair<string, IEnumerable<string>>(_s.Acronimo!,
                                 from _sb in _subowners.Where(s => s.Dominio == _s.Id)
                                 select new string(_sb.Acronimo)));



            var t = Task.Run(() => _roleManager.Roles.OrderBy(o => o.Name));
            await t;
            Input = new()
            {
                Roles = t.Result
            };

            var _rlist = from r in Input.Roles select r.Name;

            foreach (var r in _olist)
            {
                OwnerList?.Add(new(r.Key, r.Value.Except(_rlist)));
            }
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
                    var role = new IdentityRole(OwnerSelect);
                    var roleresult = await _roleManager.CreateAsync(role);

                    if (roleresult.Succeeded)
                    {
                        Input!.Roles = _roleManager.Roles;
                        OwnerSelect = string.Empty;

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
