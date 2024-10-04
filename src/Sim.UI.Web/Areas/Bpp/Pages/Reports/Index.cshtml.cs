using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Indicadores.Interfaces;
using Sim.Application.Indicadores.VModel;
using Sim.Application.Interfaces;
using Sim.Domain.Evento.Model;
using Sim.Identity.Policies;
using Sim.UI.Web.Areas.Bpp.Services;

namespace Sim.UI.Web.Areas.Bpp.Pages.Reports;

[RoleOrClaimAuthorize(Module.Name, PolicyTypes.Permission, PolicyTypes.Adm_Global)]
public class IndexModel : PageModel
{
    private readonly IAppServiceEvento _appevento;
    private readonly IAppServiceAtendimento _appatendimento;
    private readonly IAppIndicadores _appindicadores;

    [BindProperty(SupportsGet = true)]
    public EReports? LReports { get; set; }

    [BindProperty]
    public int InputAno { get; set; }

    [TempData]
    public string? StatusMessage { get; set; }

    public IndexModel(IAppServiceEvento appevento,
                    IAppServiceAtendimento appatendimento,
                    IAppIndicadores appIndicadores)  {
        _appevento = appevento; 
        _appatendimento = appatendimento;
        _appindicadores = appIndicadores;
    }
    public async Task OnGetAsync() {
        StatusMessage = "";
        InputAno = DateTime.Today.Year;
        var _list_ev = await _appevento.DoListAsync(s => s.Data!.Value.Year == InputAno && s.Owner == "Banco do Povo" && s.Situacao != EEvento.ESituacao.Cancelado);
        var _list_at = await _appatendimento.DoListAsync(s => s.Data!.Value.Year == InputAno && s.Setor == "Banco do Povo");
        LReports = await _appindicadores.DoReportAsync(_list_at, _list_ev);
    }

    public async Task OnPostAsync() {
        var _list_ev = await _appevento.DoListAsync(s => s.Data!.Value.Year == InputAno && s.Owner == "Banco do Povo" && s.Situacao != EEvento.ESituacao.Cancelado);
        var _list_at = await _appatendimento.DoListAsync(s => s.Data!.Value.Year == InputAno && s.Setor == "Banco do Povo");
        LReports = await _appindicadores.DoReportAsync(_list_at, _list_ev);
    }
}

