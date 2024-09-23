using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Sim.Application.Interfaces;
using Sim.Domain.Evento.Model;

namespace Sim.UI.Web.Pages.Agenda
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IAppServiceEvento _appServiceEvento;
        private readonly IAppServiceSecretaria _appSecretaria;

        [BindProperty(SupportsGet = true)]
        public InputModelIndex? Input { get; set; }

        public class InputModelIndex
        {
            public string? Search { get; set; }
            public IEnumerable<InputModelEvento>? ListaEventos { get; set; }
            public IEnumerable<(string Mes, int Qtde, IEnumerable<EEvento>)>? ListaEventosMes { get; set; }
        }

        //[TempData]
        public string? StatusMessage { get; set; }

        public IndexModel(IAppServiceEvento appServiceEvento,
                            IAppServiceSecretaria appSecretaria)
        {
            _appServiceEvento = appServiceEvento;
            _appSecretaria = appSecretaria;
        }

        public async Task Load(EEvento.ESituacao? m)
        {
            var _eventos_vencidos = false;
            ViewData["ActivePageEvento"] = m switch
            {
                EEvento.ESituacao.Ativo => AgendaNavPages.EventoAtivo,
                EEvento.ESituacao.Finalizado => AgendaNavPages.EventoFinalizado,
                EEvento.ESituacao.Cancelado => AgendaNavPages.EventoCancelado,
                _ => AgendaNavPages.EventoAtivo,
            };

            var _dominioativo = await _appSecretaria.DoListAsync(s => s.Acronimo == HttpContext.Session.GetString("Dominio"));

            Input!.ListaEventosMes = await _appServiceEvento
                                                    .ListEventosPorMesAsync(await
                                                                        _appServiceEvento.DoListAsync(
                                                                                s => s.Situacao == m));
            //&& s.Dominio == _dominioativo.FirstOrDefault()));

            foreach (var item in Input.ListaEventosMes)
            {
                foreach (var evento in item.Item3)
                {
                    if (evento.Data <= DateTime.Now && evento.Situacao == EEvento.ESituacao.Ativo)
                        _eventos_vencidos = true;
                }
            }

            if (_eventos_vencidos)
                StatusMessage = "Alerta: Há eventos vencidos não finalizados!";
        }

        public async Task OnGetAsync(EEvento.ESituacao? m)
        {
            var _p = m == null ? EEvento.ESituacao.Ativo : m;

            await Load(_p);
        }

        public async Task OnPostAsync()
        {
            var _dominioativo = await _appSecretaria.DoListAsync(s => s.Acronimo == HttpContext.Session.GetString("Dominio"));
            Input!.ListaEventosMes = await _appServiceEvento
                .ListEventosPorMesAsync(await _appServiceEvento
                .DoListAsync(s => s.Nome!.Contains(Input.Search!)
                                || s.Tipo!.Contains(Input.Search!)
                                || s.Parceiro!.Contains(Input.Search!)
                                || s.Descricao!.Contains(Input.Search!)
                                || s.Inscritos!.FirstOrDefault()!.Participante!.Nome!.Contains(Input.Search!)
                                || s.Inscritos!.FirstOrDefault()!.Participante!.CPF == Input.Search!));
            //&& s.Dominio == _dominioativo.FirstOrDefault()));
        }

        private int QuantosDiasFaltam(DateTime dataalvo)
        {
            return (int)dataalvo.Subtract(DateTime.Today).TotalDays;
        }
    }
}
