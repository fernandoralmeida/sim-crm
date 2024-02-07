using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Interfaces;
using Sim.Identity.Entity;
using Sim.Identity.Interfaces;

namespace Sim.UI.Web.Areas.Identity.Pages.Account.Manage
{

    [Authorize]
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IServiceUser _user;
        private readonly IAppServiceSecretaria _secretaria;
        private readonly IMapper _map;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IServiceUser user,
            IAppServiceSecretaria secretaria,
            IMapper map)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _user = user;
            _secretaria = secretaria;
            _map = map;
        }

        public string? UserName { get; set; }

        [TempData]
        public string? StatusMessage { get; set; }

        [BindProperty]
        public InputModel? Input { get; set; }

        public IEnumerable<KeyValuePair<string, IEnumerable<KeyValuePair<string, Guid>>>>? OwnerList { get; set; }

        [BindProperty]
        public string? OwnerSelect { get; set; }

        public IEnumerable<Claim>? Vinculos { get; set; }
        public IEnumerable<string>? Funcoes { get; set; }

        public class InputModel
        {
            public string? Id { get; set; }

            [Display(Name = "Nome")]
            public string? Name { get; set; }

            [Display(Name = "Sobrenome")]
            public string? LastName { get; set; }

            [Display(Name = "Gênero")]
            public string? Genero { get; set; }

            [Phone]
            [Display(Name = "Telefone")]
            public string? PhoneNumber { get; set; }
        }

        private async Task LoadAsync(string user)
        {
            var _user = await _userManager.GetUserAsync(User);
            var _roles = await _userManager.GetRolesAsync(_user);
            var _claims = await _userManager.GetClaimsAsync(_user);

            UserName = _user.UserName;

            Input = new InputModel
            {
                Id = _user.Id,
                Name = _user.Name,
                LastName = _user.LastName,
                Genero = _user.Gender,
                PhoneNumber = _user.PhoneNumber
            };

            var _owners = await _secretaria.DoListHierarquia1Async(await _secretaria.DoListAsync());
            var _subowners = await _secretaria.DoListHierarquia2Async(await _secretaria.DoListAsync());

            OwnerList = from _s in _owners.Where(s => s.Hierarquia == Domain.Organizacao.Model.EHierarquia.Secretaria)
                        select (new KeyValuePair<string, IEnumerable<KeyValuePair<string, Guid>>>(_s.Acronimo!,
                                from _sb in _subowners.Where(s => s.Dominio == _s.Id)
                                select (new KeyValuePair<string, Guid>(_sb.Acronimo!, _sb.Id))));

            Vinculos = _claims;
            Funcoes = _roles;

        }

        public async Task<IActionResult> OnGetSetTheme(string id, string theme)
        {

            if (!string.IsNullOrEmpty(id))
            {
                HttpContext.Session.SetString("Theme", theme);
                await _user.SetThemeAsync(id, theme);
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Não foi possível carregar o usuário com ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user.Id);
            return Page();
        }

        public async Task OnPostBindAccountAsync(string id)
        {
            try
            {
                var _user = await _userManager.FindByIdAsync(id);

                var _setor = await _secretaria.GetAsync(Guid.Parse(OwnerSelect!));
                Claim _claim = new(_setor.Acronimo!, _setor.Id.ToString(), ClaimValueTypes.String);

                IdentityResult result = await _userManager.AddClaimAsync(_user, _claim);

                await LoadAsync(id);
                if (result.Succeeded)
                    StatusMessage = $"Vinculo {_user.Name} :: {_setor.Acronimo} criado com sucesso!";

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

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Não foi possível carregar o usuário com ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user.Id);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input!.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Erro inesperado ao tentar definir o número de telefone.";
                    return RedirectToPage();
                }
            }

            var name_lastname = await _userManager.GetUserAsync(User);
            if (Input.Name != name_lastname.Name || Input.LastName != name_lastname.LastName || Input.Genero != name_lastname.Gender)
            {
                name_lastname.Name = Input.Name;
                name_lastname.LastName = Input.LastName;
                name_lastname.Gender = Input.Genero;
                var update_user = await _userManager.UpdateAsync(name_lastname);
                if (!update_user.Succeeded)
                {
                    StatusMessage = "Erro inesperado, tente novamente.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Seu perfil foi atualizado";
            return RedirectToPage();
        }
    }
}
