using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Sim.Domain.Evento.Model;
using Sim.Domain.Organizacao.Model;
using Sim.Application.Interfaces;

namespace Sim.UI.Web.Pages.Agenda.Eventos
{

    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceTipo _appServiceTipo;
        private readonly IAppServiceEvento _appServiceEvento;
        private readonly IAppServiceParceiro _appServiceParceiro;
        private readonly IAppServiceSecretaria _appSecretaria;
        private readonly IMapper _mapper;

        public IndexModel(IAppServiceEvento appServiceEvento,
            IAppServiceTipo appServiceTipo,
            IAppServiceParceiro appServiceParceiro,
            IAppServiceSecretaria appSecretaria,
            IMapper mapper)
        {
            _appServiceEvento = appServiceEvento;
            _appServiceTipo = appServiceTipo;
            _appServiceParceiro = appServiceParceiro;
            _appSecretaria = appSecretaria;
            _mapper = mapper;
        }

        [BindProperty(SupportsGet = true)]
        public InputModelEvento? Input { get; set; }

        [TempData]
        public string? StatusMessage { get; set; }

        public SelectList? TipoEventos { get; set; }
        public SelectList? Setores { get; set; }
        public SelectList? Parceiros { get; set; }
        public SelectList? Situacoes { get; set; }

        private async Task Onload()
        {

            var _org = await _appSecretaria.DoListAsync(s => s.Hierarquia == EHierarquia.Secretaria);

            if (_org.Count() == 0)
                return;

            var _setores = await _appSecretaria.DoListAsync(s => s.Hierarquia == EHierarquia.Setor && s.Dominio == _org!.FirstOrDefault()!.Id);
            Setores = new SelectList(_setores, nameof(EOrganizacao.Nome), nameof(EOrganizacao.Nome), null);

            var t = await _appServiceTipo.DoListAsync();

            var p = await _appServiceParceiro.DoListAsync();

            if (t != null)
            {
                TipoEventos = new SelectList(t, nameof(ETipo.Nome), nameof(ETipo.Nome), null);
            }

            if (p != null)
            {
                Parceiros = new SelectList(p, nameof(EParceiro.Nome), nameof(EParceiro.Nome), null);
            }

            Situacoes = new SelectList(Enum.GetNames(typeof(EEvento.ESituacao)));
        }
        public async Task OnGet()
        {
            await Onload();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    StatusMessage = "Verifique o preenchimento correto do formulário!";
                    await Onload();
                    return Page();
                }

                Input!.Codigo = Convert.ToInt32(DateTime.Now.ToUniversalTime());

                await _appServiceEvento.AddAsync(_mapper.Map<EEvento>(Input));

                return RedirectToPage("/Agenda/Index", new { m = "ativos" });
            }
            catch (Exception ex)
            {
                StatusMessage = "Erro: " + ex.Message;
                return Page();
            }
        }
    }
}
