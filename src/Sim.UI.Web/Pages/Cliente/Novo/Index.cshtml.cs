using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;
using AutoMapper;

namespace Sim.UI.Web.Pages.Cliente.Novo
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServicePessoa _pessoa;
        private readonly IMapper _mapper;
        public IndexModel(IAppServicePessoa appServicePessoa, IMapper mapper)
        {
            _pessoa = appServicePessoa;
            _mapper = mapper;
        }

        [TempData]
        public string? StatusMessage { get; set; }

        public string? ReturnUrl { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModelPessoa? Input { get; set; }


        public IActionResult OnGet(string? id)
        {            
            Input = new InputModelPessoa
            {
                CPF = id,
                Data_Cadastro = DateTime.Now,
                Ultima_Alteracao = DateTime.Now,
                Ativo = true                
            };

            StatusMessage = string.Empty;
            
            if(!string.IsNullOrEmpty(id)) {
                if(!Functions.Validate.IsCpf(id))
                {
                    StatusMessage = "Erro: CPF inválido!";
                    return RedirectToPage("/Cliente/Index");
                }
            };              

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                Input!.Nome = Input.Nome?.Trim();                

                var pessoa = _mapper.Map<Pessoa>(Input);

                if (Input.Fisica)
                    pessoa.Deficiencia += "Física;";

                if (Input.Visual)
                    pessoa.Deficiencia += "Visual;";

                if (Input.Auditiva)
                    pessoa.Deficiencia += "Auditiva;";

                if (Input.Intelectual)
                    pessoa.Deficiencia += "Intelectual;";

                await _pessoa.AddAsync(pessoa);

                return RedirectToPage("/Cliente/Index");

            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + "Verifique se o Cliente já está cadastrado ou contate o suporte! - " + ex.Message;
                return Page();
            }


        }


    }
}
