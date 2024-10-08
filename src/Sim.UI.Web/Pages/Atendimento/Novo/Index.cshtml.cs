using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Sim.Application.Interfaces;
using Sim.Domain.Organizacao.Model;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Sim.Identity.Entity;

namespace Sim.UI.Web.Pages.Atendimento.Novo
{

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceAtendimento _appServiceAtendimento;
        private readonly IAppServiceSecretaria _appSecretaria;
        private readonly IAppServiceCanal _appServiceCanal;
        private readonly IAppServiceServico _appServiceServico;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(IAppServiceAtendimento appServiceAtendimento,
            IAppServiceCanal appServiceCanal,
            IAppServiceServico appServiceServico,
            IAppServiceSecretaria appSecretaria,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _appServiceAtendimento = appServiceAtendimento;
            _appServiceCanal = appServiceCanal;
            _appServiceServico = appServiceServico;
            _appSecretaria = appSecretaria;
            _userManager = userManager;
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

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                await OnLoad();

                var atendimemnto_ativio = await _appServiceAtendimento.DoListAsync(s => s.Owner_AppUser_Id == User.Identity!.Name && s.Status == "Ativo");

                if (!atendimemnto_ativio.Any())
                {
                    StatusMessage = "Alerta: Não existe atendimento ativo no momento!";
                    return RedirectToPage("/Atendimento/Index");
                }

                Input = _mapper.Map<InputModelAtendimento>(atendimemnto_ativio.FirstOrDefault());

                Input!.Setor = HttpContext.Session.GetString("SetorAtivo") ?? "";
                var _canal = Canais?.FirstOrDefault(s => s.Value == "Presencial");
                Input.Canal = _canal?.Value;
                return Page();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Erro: {ex.Message}";
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

                var _dominioativo = await _appSecretaria.DoListAsync(s => s.Acronimo == HttpContext.Session.GetString("Dominio"));
                var _dominio_selecionado = await _appSecretaria.GetAsync((Guid)_dominioativo.FirstOrDefault()?.Id!);

                var atold = await _appServiceAtendimento.GetAsync(Input.Id);
                atold.DataF = DateTime.Now;
                atold.Setor = Input.Setor!;
                atold.Canal = Input.Canal;
                atold.Servicos = ServicosSelecionados;
                atold.Descricao = Input.Descricao;
                atold.Status = "Finalizado";
                atold.Ultima_Alteracao = DateTime.Now;
                atold.Dominio = _dominio_selecionado;
                await _appServiceAtendimento.UpdateAsync(atold);

                StatusMessage = "Lembre-se de incentivar os clientes a responderem nossa pesquisa de satisfação!";
                return RedirectToPage("/Atendimento/Index");

            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                await OnLoad();
                return Page();
            }

        }
    }
}
