using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Identity.Entity;
using Sim.Application.Interfaces;
using Newtonsoft.Json;

namespace Sim.UI.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAppServiceSecretaria _appSecretaria;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<ApplicationUser> signInManager,
            IAppServiceSecretaria appServiceSecretaria,
            ILogger<LoginModel> logger)
        {
            _signInManager = signInManager;
            _appSecretaria = appServiceSecretaria;
            _logger = logger;
        }

        [BindProperty]
        public InputModel? Input { get; set; }

        public IList<AuthenticationScheme>? ExternalLogins { get; set; }

        public string? ReturnUrl { get; set; }

        [TempData]
        public string? ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Identificador")]
            public string? UserName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Senha")]
            public string? Password { get; set; }

            [Display(Name = "Lembrar de mim?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string? returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();


            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Input!.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    var first_name = await _signInManager.UserManager.FindByNameAsync(Input.UserName);

                    if (first_name.LockoutEnabled)
                    {
                        _logger.LogWarning("Conta de usuário bloqueada!");
                        return RedirectToPage("./Lockout");
                    }

                    await CreateSessions(first_name);

                    _logger.LogInformation($"Usuário {User?.Identity!.Name} conectado.");

                    if (DateTime.Now.DayOfYear > 10)
                        return LocalRedirect(returnUrl);
                    else
                        return RedirectToPage("/Index");
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("Conta de usuário bloqueada.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Tentativa de login inválida.");
                    return Page();
                }
            }

            return Page();
        }

        //cria 5 session para ser usadno no titulo do app, setorativo e o tema do app.
        private async Task CreateSessions(ApplicationUser appuser)
        {
            try
            {
                var _CRM = "SimCRM";
                var _CRMID = new Guid().ToString();
                var _claims = await _signInManager.UserManager.GetClaimsAsync(appuser);
                var _list = await _appSecretaria.DoListAsync();
                var collection = new List<KeyValuePair<string, Guid>>();

                HttpContext.Session.SetString("FirstName", appuser.Name);
                HttpContext.Session.SetString("Theme", appuser.Theme ?? "light");

                foreach (var claim in _claims)
                {
                    collection.Add(new KeyValuePair<string, Guid>(claim.Type, Guid.Parse(claim.Value)));
                    var _setor = _list.Where(s => s.Id == Guid.Parse(claim.Value)).FirstOrDefault()!;
                    var _dominio = _list.Where(s => s.Id == _setor.Dominio).FirstOrDefault()!;
                    _CRM = _dominio.Acronimo ?? "SimCRM";
                    _CRMID = _dominio.Id.ToString() ?? _CRMID;
                }

                HttpContext.Session.SetString("Dominio", _CRM);
                HttpContext.Session.SetString("DominioID", _CRMID);
                HttpContext.Session.SetString("SetorAtivo", _claims.First()?.Type!);

                var json = JsonConvert.SerializeObject(collection);
                HttpContext.Session.SetString("ClaimList", json);

            }
            catch (Exception ex)
            {
                TempData["StatusMessage"] = ex.Message;
                HttpContext.Session.SetString("Dominio", "SimCRM");
                HttpContext.Session.SetString("DominioID", new Guid().ToString());
                HttpContext.Session.SetString("ClaimList", "");
                HttpContext.Session.SetString("SetorAtivo", "Setor");
            }
        }
    }
}
