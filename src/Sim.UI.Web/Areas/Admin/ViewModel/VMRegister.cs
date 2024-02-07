using System.ComponentModel.DataAnnotations;

namespace Sim.UI.Web.Areas.Admin.ViewModel
{
    public class VMRegister
    {

        [Required]
        [Display(Name = "Identificador")]
        public string? UserName { get; set; }

        [Required]
        [Display(Name = "Nome")]
        public string? Name { get; set; }

        [Required]
        [Display(Name = "Sobrenome")]
        public string? LastName { get; set; }

        [Required]
        [Display(Name = "Genero")]
        public string? Genero { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0} deve ter pelo menos {2} e no máximo {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme sua senha")]
        [Compare("Password", ErrorMessage = "A senha e a senha de confirmação não correspondem.")]
        public string? ConfirmPassword { get; set; }

        public string? StatusMessage { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
