using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

using Sim.Domain.Entity;
using Sim.Application.Interfaces;
using Sim.UI.Web.Functions;

namespace Sim.UI.Web.Pages.Cliente.Consulta
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServicePessoa _pessoaApp;

        public IndexModel(IAppServicePessoa appServicePessoa)
        {
            _pessoaApp = appServicePessoa;
        }

        [TempData]
        public string? StatusMessage { get; set; }

        public bool CpfValido = false;

        [BindProperty(SupportsGet = true)]
        public InputModel? Input { get; set; }
        public class InputModel
        {                  
            public string? Nome { get; set; }
            public IEnumerable<Pessoa>? ListaPessoas { get; set; }
        }

        public async Task< IActionResult> OnGetAsync()
        {
            Input!.ListaPessoas = await _pessoaApp.GetLastAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                                    
                Input!.ListaPessoas = await _pessoaApp.DoListAsync(s => s.Nome!.Contains(Input.Nome!));     
                if (!Input.ListaPessoas.Any())
                    StatusMessage = $"Alerta: {Input.Nome} não cadastrado(a)!";
          
            }
            catch(Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
            }
            
            return Page();          
        }

    }
}

