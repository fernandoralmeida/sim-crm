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

        [BindProperty(SupportsGet = true)]
        public InputModelIndex? Input { get; set; }

        public class InputModelIndex
        {
            public string? Search { get; set; }
            public IEnumerable<InputModelEvento>? ListaEventos { get; set; }
            public IEnumerable<(string Mes, int Qtde, IEnumerable<EEvento>)>? ListaEventosMes { get; set; }
        }

        [TempData]
        public string? StatusMessage { get; set; }

        public IndexModel(IAppServiceEvento appServiceEvento)
        {
            _appServiceEvento = appServiceEvento;
        }

        public async Task Load(EEvento.ESituacao situacao, string m)
        {
            var _eventos_vencidos = false;
            EEvento.ESituacao sto = EEvento.ESituacao.Ativo;
            switch(m)
            {
                case "ativos":
                    ViewData["ActivePageEvento"] = AgendaNavPages.EventoAtivo;
                    sto = EEvento.ESituacao.Ativo;
                    break;
                case "finalizados":
                    ViewData["ActivePageEvento"] = AgendaNavPages.EventoFinalizado;
                    sto = EEvento.ESituacao.Finalizado;
                    break;
                case "cancelados":
                    ViewData["ActivePageEvento"] = AgendaNavPages.EventoCancelado;
                    sto = EEvento.ESituacao.Cancelado;
                    break;
                default:
                    ViewData["ActivePageEvento"] = AgendaNavPages.EventoAtivo;
                    break;
            }
            Input!.ListaEventosMes = await _appServiceEvento
                .ListEventosPorMesAsync(await _appServiceEvento.DoListAsync(s => s.Situacao == sto));

            foreach(var item in Input.ListaEventosMes){
                foreach(var evento in item.Item3){
                    if(evento.Data <= DateTime.Now && evento.Situacao == EEvento.ESituacao.Ativo)
                        _eventos_vencidos = true;
                }
            }

            if(_eventos_vencidos)
                StatusMessage = "Alerta: Há eventos vencidos não finalizados!";
        }

        public async Task OnGetAsync(string m)
        {            
            var _p = m! == null ? "ativos" : m;

            await Load(EEvento.ESituacao.Ativo, _p);
        }

        public async Task OnPostAsync()
        {
            Input!.ListaEventosMes = await _appServiceEvento
                .ListEventosPorMesAsync(await _appServiceEvento
                .DoListAsync(s => s.Nome!.Contains(Input.Search!) 
                                || s.Tipo!.Contains(Input.Search!)
                                || s.Parceiro!.Contains(Input.Search!)
                                || s.Descricao!.Contains(Input.Search!)
                                || s.Inscritos!.FirstOrDefault()!.Participante!.Nome!.Contains(Input.Search!)
                                || s.Inscritos!.FirstOrDefault()!.Participante!.CPF == Input.Search!));
        }

        private int QuantosDiasFaltam(DateTime dataalvo)
        {
            return (int)dataalvo.Subtract(DateTime.Today).TotalDays;
        }
    }
}
