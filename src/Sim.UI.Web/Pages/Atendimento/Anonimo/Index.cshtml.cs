using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sim.Application.Interfaces;
using Sim.Domain.Entity;
using Sim.Domain.Organizacao.Model;
using Sim.Identity.Entity;

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
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(IAppServiceAtendimento appServiceAtendimento,
            IAppServiceCanal appServiceCanal,
            IAppServiceServico appServiceServico,
            IAppServiceSecretaria appSecretaria,
            IAppServiceContador appServiceContador,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _appServiceAtendimento = appServiceAtendimento;
            _appServiceCanal = appServiceCanal;
            _appServiceServico = appServiceServico;
            _appSecretaria = appSecretaria;
            _appServiceContador = appServiceContador;
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
            try
            {
                var _claims = await
                                _userManager.GetClaimsAsync(
                                    await _userManager.GetUserAsync(User));

                var _setores = new List<EOrganizacao>();

                foreach (var claim in _claims)
                    _setores.Add(await _appSecretaria!.GetAsync(Guid.Parse(claim.Value)));

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
                        item.Type, from c in await _appServiceServico.DoListAsync(s => s.Dominio!.Id == Guid.Parse(item.Value))
                                   select c.Nome
                    ));

                ListaServicos = _lista;

            }
            catch (Exception ex)
            {
                StatusMessage = $"Erro: {ex.Message}";
            }
        }

        public async Task OnGetAsync()
        {
            await OnLoad();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            // try
            // {

            if (Input!.Servicos == null || Input.Servicos == string.Empty)
            {
                StatusMessage = "Alerta: " + "Selecione um serviço ou mais!";
                await OnLoad();
                return RedirectToPage();
            }

            var _dominioativo = await _appSecretaria.DoListAsync(s => s.Acronimo == HttpContext.Session.GetString("Dominio"));
            var _dominio_selecionado = await _appSecretaria.GetAsync((Guid)_dominioativo.FirstOrDefault()?.Id!);

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
                Owner_AppUser_Id = User.Identity.Name,
                Dominio = _dominio_selecionado
            };

            await _appServiceAtendimento.AddAsync(_anonimo);

            return RedirectToPage("/Atendimento/Index");

            // }
            // catch (Exception ex)
            // {
            //     StatusMessage = "Erro: " + ex.Message;
            //     await OnLoad();
            //     return Page();
            // }

        }

    }
}