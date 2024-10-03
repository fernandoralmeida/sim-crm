using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using Sim.UI.Web.Areas.Admin.ViewModel;
using Sim.Identity.Entity;
using Sim.Identity.Config;
using AutoMapper;
using Sim.Application.Interfaces;

using System.Security.Claims;

namespace Sim.UI.Web.Areas.Admin.Pages.Manager
{

    [Authorize(Policy = "AdminOrAccounts")]
    public class UserRolesModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAppServiceSecretaria _secretaria;
        private readonly IMapper _map;

        public UserRolesModel(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IAppServiceSecretaria secretaria,
            IMapper map)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _secretaria = secretaria;
            _map = map;
        }

        [TempData]
        public string? StatusMessage { get; set; }

        [BindProperty]
        public VMUserRoles? Input { get; set; }

        [BindProperty]
        public string? Modulos { get; set; }

        [BindProperty]
        public string? ResetCode { get; set; }

        [Required]
        [BindProperty]
        public string? Selecionado { get; set; }

        public SelectList? RoleList { get; set; }
        public IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<string, string>>>>? OwnerList { get; set; }

        [BindProperty]
        public string? OwnerSelect { get; set; }

        private async Task LoadAsync(string id)
        {
            // var _owners = await _secretaria.DoListHierarquia1Async(await _secretaria.DoListAsync());
            // var _subowners = await _secretaria.DoListHierarquia2Async(await _secretaria.DoListAsync());

            // OwnerList = from _s in _owners.Where(s => s.Hierarquia == Domain.Organizacao.Model.EHierarquia.Secretaria)
            //             select (new KeyValuePair<string, IEnumerable<KeyValuePair<string, Guid>>>(_s.Acronimo!,
            //                     from _sb in _subowners.Where(s => s.Dominio == _s.Id)
            //                     select (new KeyValuePair<string, Guid>(_sb.Acronimo!, _sb.Id))));

            OwnerList = new List<KeyValuePair<string, IEnumerable<KeyValuePair<string, string>>>>
            {
                new(
                    "Permissions", new List<KeyValuePair<string, string>>
                    {
                        new("Permission", "Adm_Settings"),
                        new("Permission", "Adm_Accounts")
                    })
            };

            var roles = _roleManager.Roles.ToList();
            if (User.IsInRole(AccountType.Adm_Global))
                RoleList = new SelectList(roles.OrderBy(o => o.Name), nameof(IdentityRole.Name));
            else
                RoleList = new SelectList(roles.Where(s => s.Name != AccountType.Adm_Global && s.Name != AccountType.Adm_Account).OrderBy(o => o.Name), nameof(IdentityRole.Name));

            var u = await _userManager.FindByIdAsync(id);
            var r = await _userManager.GetRolesAsync(u);
            var c = await _userManager.GetClaimsAsync(u);

            Input = new()
            {
                Id = u.Id,
                UserName = u.UserName,
                Name = u.Name,
                LastName = u.LastName,
                Gender = u.Gender,
                Email = u.Email,
                EmailConfirmed = u.EmailConfirmed,
                ListRoles = r,
                ListClaims = c
            };
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            await LoadAsync(id);
            var user = await _userManager.FindByEmailAsync(Input!.Email);
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            ResetCode = code;
            return Page();
        }

        public async Task<IActionResult> OnPostConfirmEmailAsync(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                await _userManager.ConfirmEmailAsync(user, code);

                return RedirectToPage("./UserRoles", new { id });
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
                await LoadAsync(id);
                return Page();
            }

        }

        public async Task<IActionResult> OnPostAddRoleAsync(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);

                await _userManager.AddToRoleAsync(user, Selecionado);

                return RedirectToPage("./UserRoles", new { id });
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
                await LoadAsync(id);
                return Page();
            }

        }

        public async Task<IActionResult> OnPostRemoveRoleAsync(string id, string role)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);

                await _userManager.RemoveFromRoleAsync(user, role);

                return RedirectToPage("./UserRoles", new { id = user.Id });
            }
            catch
            {
                return Page();
            }
        }

        public async Task OnPostBindAccountAsync(string id)
        {
            try
            {
                var _user = await _userManager.FindByIdAsync(id);

                // var _setor = await _secretaria.GetAsync(Guid.Parse(OwnerSelect!));

                // var _clains = await _userManager.GetClaimsAsync(_user);

                // foreach (var item in _clains)
                // {
                //     var _verificardominio = await _secretaria.GetAsync(Guid.Parse(item.Value));
                //     if (_verificardominio != null && _setor.Dominio != _verificardominio.Dominio)
                //     {
                //         var _userdominio = await _secretaria.GetAsync((Guid)_verificardominio.Dominio!);
                //         throw new Exception($"Usuário vinculado ao Dominio {_userdominio.Acronimo}!");
                //     }
                // }

                Claim _claim = new("Permission", OwnerSelect! ,ClaimValueTypes.String);

                IdentityResult result = await _userManager.AddClaimAsync(_user, _claim);

                await LoadAsync(id);
                if (result.Succeeded)
                    StatusMessage = $"Função {OwnerSelect} associada com sucesso!";

                else
                    StatusMessage = $"Erro: {result}";

                await LoadAsync(id);
            }
            catch (Exception ex)
            {
                await LoadAsync(id);
                StatusMessage = $"Erro: {ex.Message} : {OwnerSelect}";
            }
        }

        public async Task OnPostUnbindAccountAsync(string id, string ct, string cv)
        {
            try
            {
                var _claim = new Claim(ct, cv);
                var _user = await _userManager.FindByIdAsync(id);
                await _userManager.RemoveClaimAsync(_user, _claim);
                await LoadAsync(id);
            }
            catch (Exception ex)
            {
                await LoadAsync(id);
                StatusMessage = $"Erro: {ex.Message} : {id}";
            }
        }
    }
}
