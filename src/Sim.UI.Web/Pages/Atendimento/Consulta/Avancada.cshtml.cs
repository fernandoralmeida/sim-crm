using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

using Sim.Domain.Organizacao.Model;
using Sim.Application.Interfaces;
using Sim.Identity.Interfaces;
using Sim.Identity.Entity;
using Sim.Domain.Entity;

namespace Sim.UI.Web.Pages.Atendimento.Consulta
{
    [Authorize]
    public class AvancadaModel : PageModel
    {
        private readonly IAppServiceAtendimento _appServiceAtendimento;
        private readonly IAppServiceServico _appServiceServico;
        private readonly IServiceUser _appIdentity;
        private readonly IAppServiceSecretaria _appServiceSecretaria;

        [TempData]
        public string? StatusMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public InputModel? Input { get; set; }
        public SelectList? ListaAtendentes { get; set; }
        public SelectList? ListaServicos { get; set; }
        public SelectList? ListaSetores { get; set; }

        public class InputModel
        {
            [DisplayName("Data Inicial")]
            [DataType(DataType.DateTime)]
            public DateTime DataI { get; set; }

            [DisplayName("Data Final")]
            [DataType(DataType.DateTime)]
            public DateTime DataF { get; set; }

            public string? CPF { get; set; }
            public string? CNPJ { get; set; }
            public string? Nome { get; set; }

            [DisplayName("Razão Social")]
            public string? RazaSocial { get; set; }
            public string? CNAE { get; set; }
            public string? Servico { get; set; }
            public string? Atendente { get; set; }
            public string? Setor { get; set; }

            public ICollection<EAtendimento>? ListaAtendimento { get; set; }
        }

        public AvancadaModel(IAppServiceAtendimento appServiceAtendimento,
            IServiceUser appServiceUser,
            IAppServiceServico appServiceServico,
            IAppServiceSecretaria appServiceSecretaria)
        {
            _appServiceAtendimento = appServiceAtendimento;
            _appIdentity = appServiceUser;
            _appServiceServico = appServiceServico;
            _appServiceSecretaria = appServiceSecretaria;
            Input = new();
        }

        public async Task OnGetAsync()
        {
            Input!.DataI = new DateTime(DateTime.Now.Year, 1, 1, 0, 1, 0);
            Input!.DataF = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);
            Input.ListaAtendimento = new List<EAtendimento>();
            await LoadUsers();
            await LoadServicos();
            await LoadSetores();
        }

        private async Task LoadUsers()
        {
            ListaAtendentes = new SelectList(await _appIdentity.ListAllAsync(), nameof(ApplicationUser.UserName), nameof(ApplicationUser.UserName), null);
        }

        private async Task LoadServicos()
        {
            ListaServicos = new SelectList(await _appServiceServico.DoListAsync(), nameof(EServico.Nome), nameof(EServico.Nome), null);
        }

        public async Task LoadSetores()
        {
            var _org = await _appServiceSecretaria.DoListAsync(s => s.Hierarquia == EHierarquia.Secretaria);
            if (_org.Count() == 0)
                return;

            var _setores = await _appServiceSecretaria.DoListAsync(s => s.Hierarquia == EHierarquia.Setor && s.Dominio == _org.FirstOrDefault()!.Id);
            ListaSetores = new SelectList(_setores, nameof(EOrganizacao.Nome), nameof(EOrganizacao.Nome), null);
        }


        public async Task OnPostAsync()
        {
            try
            {
                var param = new List<object>() {
                    Input!.DataI,
                    Input.DataF,
                    Input.CPF!,
                    Input.Nome!,
                    Input.CNPJ!,
                    Input.RazaSocial!,
                    Input.CNAE!,
                    Input.Servico!,
                    Input.Atendente!,
                    Input.Setor! };

                Input.ListaAtendimento = (ICollection<EAtendimento>)await _appServiceAtendimento.ListParamAsync(param);
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                Input!.ListaAtendimento = new List<EAtendimento>();
            }

            await LoadServicos();
            await LoadUsers();
            await LoadSetores();
        }
    }
}
