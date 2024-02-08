using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

using Sim.Domain.Entity;
using Sim.Application.Interfaces;
using Sim.UI.Web.Functions;

namespace Sim.UI.Web.Pages.Cliente
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
            public string? Valor { get; set; }
            public string? RouteCPF { get; set; }
            public IEnumerable<Pessoa>? ListaPessoas { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            //por default mostra os 10 ultimos registros
            Input!.ListaPessoas = await _pessoaApp.GetLastAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                Input!.Valor = Input.Valor?.Trim();

                if (Input!.Valor!.MaskRemove().All(char.IsDigit))
                {
                    if (Validate.IsCpf(Input.Valor!))
                    {
                        CpfValido = true;

                        var _valor = Input.Valor!.MaskRemove();

                        if (_valor.Length == 11)
                            _valor = _valor.Mask("###.###.###-##");

                        Input.ListaPessoas = await _pessoaApp
                                                    .DoListAsync(s => s.CPF == _valor);

                        if (!Input.ListaPessoas.Any())
                        {
                            Input.RouteCPF = Input.Valor!.MaskRemove();
                            throw new Exception($"Alerta: CPF {Input.Valor} não cadastrado!");
                        }

                    }
                    else
                    {
                        throw new Exception($"Erro: CPF {Input.Valor} inválido!");
                    }
                }
                else
                {
                    CpfValido = false;
                    Input.ListaPessoas = await _pessoaApp
                                                    .DoListAsync(c => 
                                                        c.Nome!.Contains(Input.Valor!)
                                                        || c.Nome_Social!.Contains(Input.Valor!));
                    if (!Input.ListaPessoas.Any())
                        throw new Exception($"Alerta: {Input.Valor} não cadastrado(a)!");
                }
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
            }

            return Page();
        }

    }
}

