﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Identity.Entity;

namespace Sim.UI.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginWithRecoveryCodeModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginWithRecoveryCodeModel> _logger;

        public LoginWithRecoveryCodeModel(SignInManager<ApplicationUser> signInManager, ILogger<LoginWithRecoveryCodeModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel? Input { get; set; }

        public string? ReturnUrl { get; set; }

        public class InputModel
        {
            [BindProperty]
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Código de recuperação")]
            public string? RecoveryCode { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string? returnUrl = null)
        {
            // Ensure the user has gone through the username & password screen first
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new InvalidOperationException($"Não é possível carregar o usuário de autenticação de dois fatores.");
            }

            ReturnUrl = returnUrl;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync() ?? throw new InvalidOperationException($"Não é possível carregar o usuário de autenticação de dois fatores.");
            
            var recoveryCode = Input!.RecoveryCode!.Replace(" ", string.Empty);

            var result = await _signInManager.TwoFactorRecoveryCodeSignInAsync(recoveryCode);

            if (result.Succeeded)
            {
                _logger.LogInformation("Usuário com ID '{UserId}' conectado com um código de recuperação.", user.Id);
                return LocalRedirect(returnUrl ?? Url.Content("~/"));
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning("Usuário com conta de ID '{UserId}' bloqueada.", user.Id);
                return RedirectToPage("./Lockout");
            }
            else
            {
                _logger.LogWarning("Código de recuperação inválido inserido para o usuário com ID '{UserId}' ", user.Id);
                ModelState.AddModelError(string.Empty, "Código de recuperação inválido inserido.");
                return Page();
            }
        }
    }
}
