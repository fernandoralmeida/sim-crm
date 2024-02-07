using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;
using Sim.Domain.Organizacao.Model;

namespace Sim.UI.Web.Pages.Atendimento.Anonimo
{
    public class IndexModel : PageModel
    {
        private readonly IAppServiceAtendimento _appServiceAtendimento;
        private readonly IAppServiceSecretaria _appSecretaria;
        //private readonly IAppServiceSetor _appServiceSetor;
        private readonly IAppServiceCanal _appServiceCanal;
        private readonly IAppServiceServico _appServiceServico;
        private readonly IAppServiceContador _appServiceContador;
        private readonly IMapper _mapper;

        public IndexModel(IAppServiceAtendimento appServiceAtendimento,
            IAppServiceCanal appServiceCanal,
            IAppServiceServico appServiceServico,
            IAppServiceSecretaria appSecretaria,
            IAppServiceContador appServiceContador,
            IMapper mapper)
        {
            _appServiceAtendimento = appServiceAtendimento;
            _appServiceCanal = appServiceCanal;
            _appServiceServico = appServiceServico;
            _appSecretaria = appSecretaria;
            _appServiceContador = appServiceContador;
            _mapper = mapper;
        }

        [BindProperty(SupportsGet = true)]
        public InputModelAtendimento? Input { get; set; }

        [TempData]
        public string? StatusMessage { get; set; }

        [Required(ErrorMessage = "Selecione o setor do atendimento!")]
        [BindProperty(SupportsGet = true)]
        public string? GetSetor { get; set; }

        [BindProperty]
        public string? GetCNPJ { get; set; }
        public SelectList? Setores { get; set; }
        public SelectList? Canais { get; set; }

        public string? GetServico { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? ServicosSelecionados { get; set; }

        private async Task OnLoad()
        {
            var _org = await _appSecretaria.DoListAsync(s => s.Hierarquia == EHierarquia.Secretaria);

            if (_org.Count() == 0)
                return;

            var _setores = await _appSecretaria.DoListAsync(s => s.Hierarquia == EHierarquia.Setor && s.Dominio == _org.FirstOrDefault()!.Id);
            var _canais = await _appServiceCanal.DoListAsync(s => s.Dominio!.Id == _org.FirstOrDefault()!.Id);

            Setores = new SelectList(_setores, nameof(EOrganizacao.Nome), nameof(EOrganizacao.Nome), null);
            Canais = new SelectList(_canais, nameof(ECanal.Nome), nameof(ECanal.Nome), null);
        }

        public async Task OnGetAsync()
        {
            await OnLoad();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            try
            {

                if (Input!.Servicos == null || Input.Servicos == string.Empty)
                {
                    StatusMessage = "Alerta: " + "Selecione um serviço ou mais!";
                    await OnLoad();
                    return RedirectToPage();
                }

                var _anonimo = new EAtendimento()
                {
                    Protocolo = await _appServiceContador.GetProtocoloAsync(User.Identity!.Name!, "Atendimento Anônimo"),
                    Data = DateTime.Now,
                    DataF = DateTime.Now,
                    Status = "Finalizado",
                    Setor = Input.Setor,
                    Canal = Input.Canal,
                    Servicos = ServicosSelecionados,
                    Descricao = Input.Descricao,
                    Anonimo = true,
                    Ativo = true,
                    Ultima_Alteracao = DateTime.Now,
                    Owner_AppUser_Id = User.Identity.Name
                };

                await _appServiceAtendimento.AddAsync(_anonimo);

                return RedirectToPage("/Atendimento/Index");

            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                return Page();
            }

        }

    }
}