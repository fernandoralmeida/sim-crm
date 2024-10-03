using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Sim.Application.Interfaces;
using Sim.Domain.Organizacao.Model;
using Microsoft.AspNetCore.Identity;
using Sim.Identity.Entity;

namespace Sim.UI.Web.Pages.Atendimento.Manager
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceAtendimento _appServiceAtendimento;
        private readonly IAppServiceSecretaria _appSecretaria;
        private readonly IAppServiceCanal _appServiceCanal;
        private readonly IAppServiceServico _appServiceServico;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(IAppServiceAtendimento appServiceAtendimento,
            IAppServiceCanal appServiceCanal,
            IAppServiceSecretaria appSecretaria,
            IAppServiceServico appServiceServico,
            UserManager<ApplicationUser> userManager)
        {
            _appServiceAtendimento = appServiceAtendimento;
            _appServiceCanal = appServiceCanal;
            _appServiceServico = appServiceServico;
            _appSecretaria = appSecretaria;
            _userManager = userManager;
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
        public IEnumerable<KeyValuePair<string, IEnumerable<string>>>? ListaServicos { get; set; }
        public string? GetServico { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? ServicosSelecionados { get; set; }

        private async Task OnLoad()
        {

            var _claims = await
                            _userManager.GetRolesAsync(
                                await _userManager.GetUserAsync(User));

            var _setores = new List<EOrganizacao>();

            foreach (var claim in _claims)
            {
                var _result = await _appSecretaria!.DoListAsync(s => s.Acronimo == claim);
                _setores.Add(_result.FirstOrDefault()!);
            }

            var _sec = await _appSecretaria.DoListAsync(s => s.Dominio == _setores.FirstOrDefault()!.Dominio);

            var _canais = await _appServiceCanal.DoListAsync(s => s.Dominio!.Id == _sec.FirstOrDefault()!.Dominio);

            Setores = new SelectList(_setores, nameof(EOrganizacao.Nome), nameof(EOrganizacao.Nome), null);
            Canais = new SelectList(_canais, nameof(ECanal.Nome), nameof(ECanal.Nome), null);

            var _dominio = HttpContext.Session.GetString("Dominio");

            var _lista = new List<KeyValuePair<string, IEnumerable<string>>>
                {
                    new(_dominio!,
                        from d in await _appServiceServico.DoListAsync(s => s.Dominio!.Acronimo == _dominio)
                        select d.Nome)
                };

            foreach (var item in _claims)
                _lista.Add(new KeyValuePair<string, IEnumerable<string>>(
                    item, from c in await _appServiceServico.DoListAsync(s => s.Dominio!.Acronimo == item)
                          select c.Nome
                ));

            ListaServicos = _lista;
        }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            try
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
                    Empresa = atendimemnto_ativio.Empresa,
                    Dominio = atendimemnto_ativio.Dominio
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
            catch (Exception ex)
            {
                StatusMessage = $"Erro: {ex.Message}";
                return Page();

            }

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
                await OnLoad();
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
