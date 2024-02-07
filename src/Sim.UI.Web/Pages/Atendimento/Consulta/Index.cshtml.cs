using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

using Sim.Domain.Entity;
using Sim.Application.Interfaces;
using Sim.Identity.Interfaces;

namespace Sim.UI.Web.Pages.Atendimento.Consulta
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceAtendimento _appServiceAtendimento;
        private readonly IAppServiceServico _appServiceServico;
        private readonly IServiceUser _appIdentity;

        [TempData]
        public string? StatusMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Src { get; set; }

        public IEnumerable<EAtendimento>? ListaAtendimento { get; set; }

        public IndexModel(IAppServiceAtendimento appServiceAtendimento,
            IServiceUser appServiceUser,
            IAppServiceServico appServiceServico)
        {
            _appServiceAtendimento = appServiceAtendimento;
            _appIdentity = appServiceUser;
            _appServiceServico = appServiceServico;
        }

        public async Task OnGetAsync()
        {
             await LoadData();   
        }

        public async Task LoadData()
        {
            var lista = await _appServiceAtendimento.DoListAsync(s => 
                                            s.Pessoa!.CPF!.Contains(Src!) ||
                                            s.Pessoa.Nome!.Contains(Src!) ||
                                            s.Pessoa.Nome_Social!.Contains(Src!) || 
                                            s.Empresa!.Nome_Empresarial!.Contains(Src!) ||
                                            s.Empresa.Nome_Fantasia!.Contains(Src!) || 
                                            s.Empresa.CNPJ!.Contains(Src!));

            ListaAtendimento = lista.ToList();
        }
        public async Task OnPostAsync()
        {
            try
            {
                await LoadData();
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                ListaAtendimento = new List<EAtendimento>();
            }
        }
    }
}
