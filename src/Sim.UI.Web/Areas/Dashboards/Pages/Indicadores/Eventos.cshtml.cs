using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Indicadores.Interfaces;
using Sim.Application.Indicadores.VModel;
using Sim.Application.Interfaces;
using Sim.Domain.Helpers;
using Sim.Domain.Organizacao.Model;

namespace Sim.UI.Web.Areas.Dashboards.Pages.Indicadores;

[Authorize]
public class DashEventos : PageModel
{
    private readonly IAppIndicadores _indicadores;
    private readonly IAppServiceSecretaria _organizacao;
    public (int Ano, string Setor, string Page, IEnumerable<string> Setores) NavBar { get; set; }
    public VmREventos? LReports { get; set; }
    public int Inscritos { get; set; }
    public int Participantes { get; set; }
    public int Ausentes { get; set; }
    public string? FaixaEtaria { get; set; }
    public string? FaixaEtariaParticipantes { get; set; }
    public string[]? ParticipantesGenero { get; set; }
    public string[]? ParticipantesGeneroPresente { get; set; }
    public string? EventosSetores { get; set; }
    public string? EventosMeses { get; set; }
    public string? EventosMesesParticipantes { get; set; }
    public string? EventosMesesInscritos { get; set; }
    public string[]? ListaEventos { get; set; }
    public string[]? Eventos {get; set; }


    public DashEventos(IAppIndicadores indicadores,
        IAppServiceSecretaria organizacao)
    {
        _indicadores = indicadores;
        _organizacao = organizacao;
    }
    public async Task OnGetAsync(int ano = 0, string? setor = null)
    {
        setor ??= "SEDEMPI";
        ano = ano == 0 ? DateTime.Now.Year : ano;
        NavBar = (ano, setor, "Eventos", await Setores());
        LReports = setor == "SEDEMPI" ?
        await _indicadores.DoEventosAsync(s => s.Data!.Value.Year == ano) :
        await _indicadores.DoEventosAsync(s => s.Data!.Value.Year == ano && s.Owner == setor);

        Inscritos = LReports!.Inscritos!.Value.Value;
        Participantes = LReports!.Presentes!.Value.Value;
        Ausentes = Inscritos - Participantes;

        string[]? _eventos = new string[2];
        foreach (var x in LReports.Eventos!)
        {
            _eventos[0] += string.Format(@"{0},", x.Value);
            _eventos[1] += string.Format(@"`{0}`,", x.Key);
        }

        Eventos = _eventos;

        string[]? _cli_genero = new string[2];
        foreach (var x in LReports.ParticipantesGenero!)
        {
            _cli_genero[0] += string.Format(@"{0},", x.Value);
            _cli_genero[1] += string.Format(@"`{0}`,", x.Key);
        }

        string[]? _cli_genero_presentes = new string[2];
        foreach (var x in LReports.ParticipantesGeneroPresente!)
        {
            _cli_genero_presentes[0] += string.Format(@"{0},", x.Value);
            _cli_genero_presentes[1] += string.Format(@"`{0}`,", x.Key);
        }

        ParticipantesGenero = _cli_genero;
        ParticipantesGeneroPresente = _cli_genero_presentes;

        string? _eve_month = string.Empty;
        foreach (var m in LReports.EventosMeses!)
            _eve_month += string.Format(@"{{x:`{0}`,y:{1}}},", m.Key, m.Value);

        string? _eve_month_inscritos = string.Empty;
        foreach (var m in LReports.EventosMesesInscritos!)
            _eve_month_inscritos += string.Format(@"{{x:`{0}`,y:{1}}},", m.Key, m.Value);

        string? _eve_month_participantes = string.Empty;
        foreach (var m in LReports.EventosMesesParticipantes!)
            _eve_month_participantes += string.Format(@"{{x:`{0}`,y:{1}}},", m.Key, m.Value);

        EventosMeses = _eve_month.Length > 0 ? _eve_month[..^1] : _eve_month;
        EventosMesesInscritos = _eve_month_inscritos.Length > 0 ? _eve_month_inscritos[..^1] : _eve_month_inscritos;
        EventosMesesParticipantes = _eve_month_participantes.Length > 0 ? _eve_month_participantes[..^1] : _eve_month_participantes;

        string? _eve_setores = string.Empty;
        foreach (var m in LReports.EventosSetores!)
            _eve_setores += string.Format(@"{{x:`{0}`,y:{1}}},", m.Key, m.Value);

        EventosSetores = _eve_setores.Length > 0 ? _eve_setores[..^1] : _eve_setores;

        string? _eve_faixa_etaria = string.Empty;
        foreach (var x in LReports.FaixaEtaria!)
            _eve_faixa_etaria += string.Format(@"{{x:`{0}`,y:{1}}},", x.Key.NormalizeText(), x.Value);

        FaixaEtaria = _eve_faixa_etaria.Length > 0 ? _eve_faixa_etaria[..^1] : _eve_faixa_etaria;

        string? _eve_faixa_etaria_participantes = string.Empty;
        foreach (var x in LReports.FaixaEtariaPresentes!)
            _eve_faixa_etaria_participantes += string.Format(@"{{x:`{0}`,y:{1}}},", x.Key.NormalizeText(), x.Value);

        FaixaEtariaParticipantes = _eve_faixa_etaria_participantes.Length > 0 ? _eve_faixa_etaria_participantes[..^1] : _eve_faixa_etaria_participantes;

    }

    private async Task<IEnumerable<string>> Setores()
    {
        return from st in await _organizacao.DoListAsync(s => s.Hierarquia >= EHierarquia.Secretaria)
               select st.Acronimo;
    }
}