using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Sim.Application.Interfaces;
using Sim.Domain.Organizacao.Model;

namespace Sim.UI.Web.Pages.Atendimento.Manager
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceAtendimento _appServiceAtendimento;
        private readonly IAppServiceSecretaria _appSecretaria;
        private readonly IAppServiceCanal _appServiceCanal;
        private readonly IAppServiceServico _appServiceServico;

        public IndexModel(IAppServiceAtendimento appServiceAtendimento,
            IAppServiceCanal appServiceCanal,
            IAppServiceSecretaria appSecretaria,
            IAppServiceServico appServiceServico)
        {
            _appServiceAtendimento = appServiceAtendimento;
            _appServiceCanal = appServiceCanal;
            _appServiceServico = appServiceServico;
            _appSecretaria = appSecretaria;
        }

        [BindProperty(SupportsGet = true)]
        public InputModelAtendimento? Input { get; set; }

        [TempData]
        public string? StatusMessage { get; set; }

        [Required(ErrorMessage = "Selecione o setor do atendimento!")]
        [BindProperty(SupportsGet = true)]
        public string? GetSetor { get; set; }

        public SelectList? Setores { get; set; }
        public SelectList? Canais { get; set; }
        public SelectList? Servicos { get; set; }
        public string? GetServico { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? ServicosSelecionados { get; set; }

        private async Task OnLoad()
        {
            var _org = await _appSecretaria.DoListAsync(s => s.Hierarquia == EHierarquia.Secretaria);
            var _setores = await _appSecretaria.DoListAsync(s => s.Hierarquia == EHierarquia.Setor && s.Dominio == _org.FirstOrDefault()!.Id);
            var _canais = await _appServiceCanal.DoListAsync(s => s.Dominio!.Id == _org.FirstOrDefault()!.Id);

            Setores = new SelectList(_setores, nameof(EOrganizacao.Nome), nameof(EOrganizacao.Nome), null);
            Canais = new SelectList(_canais, nameof(ECanal.Nome), nameof(ECanal.Nome), null);
        }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            await OnLoad();

            var atendimemnto_ativio = await _appServiceAtendimento.GetAsync((Guid)id!);

            if (atendimemnto_ativio.Owner_AppUser_Id != User.Identity!.Name)
            {
                StatusMessage = $"Erro: Atendimento pertence a {atendimemnto_ativio.Owner_AppUser_Id}!";
                return RedirectToPage("/Atendimento/Index");
            }

            if (atendimemnto_ativio == null)
            {
                StatusMessage = "Erro: Algo inesperado aconteceu, tente novamente!";
                return RedirectToPage("/Atendimento/Index");
            }

            Input = new()
            {
                Id = atendimemnto_ativio.Id,
                Protocolo = atendimemnto_ativio.Protocolo,
                Data = atendimemnto_ativio.Data,
                DataF = atendimemnto_ativio.DataF,
                Setor = atendimemnto_ativio.Setor,
                Canal = atendimemnto_ativio.Canal,
                Servicos = atendimemnto_ativio.Servicos,
                Descricao = atendimemnto_ativio.Descricao,
                Status = atendimemnto_ativio.Status,
                Ultima_Alteracao = atendimemnto_ativio.Ultima_Alteracao,
                Ativo = atendimemnto_ativio.Ativo,
                Owner_AppUser_Id = atendimemnto_ativio.Owner_AppUser_Id,
                Pessoa = atendimemnto_ativio.Pessoa,
                Empresa = atendimemnto_ativio.Empresa
            };

            var serv = await _appServiceServico.DoListAsync(
                                    p => p.Dominio!.Nome!.Contains(Input.Setor!) ||
                                    p.Dominio == null);

            if (serv != null)
            {
                Servicos = new SelectList(serv, nameof(EServico.Nome), nameof(EServico.Nome), null);
            }

            Input.Canal = atendimemnto_ativio.Canal;
            Input.Servicos = atendimemnto_ativio.Servicos;
            ServicosSelecionados = atendimemnto_ativio.Servicos;

            return Page();
        }

        public async Task<IActionResult> OnPostAlterarAsync(Guid id)
        {

            try
            {

                var atold = await _appServiceAtendimento.GetAsync(id);
                atold.Setor = Input!.Setor;
                atold.Canal = Input!.Canal;

                if (Input.Servicos != null || Input.Servicos != string.Empty)
                    atold.Servicos = ServicosSelecionados;
                else
                    atold.Servicos = Input.Servicos;

                atold.Descricao = Input.Descricao;
                atold.Status = "Finalizado";
                atold.Ultima_Alteracao = DateTime.Now;
                await _appServiceAtendimento.UpdateAsync(atold);

                return RedirectToPage("/Atendimento/Index");

            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                return Page();
            }

        }

        public async Task<IActionResult> OnPostExcluirAsync(Guid id)
        {

            try
            {
                var atold = await _appServiceAtendimento.GetAsync(id);
                await _appServiceAtendimento.RemoveAsync(atold);

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
