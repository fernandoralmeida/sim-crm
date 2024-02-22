using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sim.Application.Indicadores.Interfaces;
using Sim.Application.Indicadores.VModel;
using Sim.Application.Interfaces;
using Sim.Domain.Helpers;

namespace Sim.UI.Web.Areas.Dashboards.Pages.Indicadores;

[Authorize]
public class DashAtendimentos : PageModel
{
    private readonly IAppIndicadores _indicadores;
    private readonly IAppServiceSecretaria _organizacao;
    public VmRAtendimentos? LReports { get; set; }
    public (int Ano, string Setor, string Page, IEnumerable<string> Setores) NavBar { get; set; }
    public string? AtendimentosMonth { get; set; }
    public string[]? Perfil { get; set; }
    public string[]? Canais { get; set; }
    public string? ServicosMonth { get; set; }
    public string? AtTimeDay { get; set; }
    public string? SvTimeDay { get; set; }
    public string? ClientesDistintos { get; set; }
    public string? EmpresasDistintas { get; set; }
    public string? PerfilDistintos { get; set; }
    public string[]? Genero { get; set; }
    public string? FaixaEtaria { get; set; }
    public string[]? Segmentos { get; set; }
    public string? FaixaEtariaEmpresas { get; set; }
    public string[]? AtendimentosDistintos { get; set; }

    public DashAtendimentos(IAppIndicadores indicadores,
        IAppServiceSecretaria organizacao)
    {
        _indicadores = indicadores;
        _organizacao = organizacao;
    }

    public async Task OnGetAsync(int ano = 0, string? setor = null)
    {
        setor ??= "SEDEMPI";
        ano = ano == 0 ? DateTime.Now.Year : ano;
        NavBar = (ano, setor, "Atendimentos", await Setores());
        LReports = setor == "SEDEMPI" ?
            await _indicadores.DoAtendimentosAsync(s => s.Data!.Value.Year == ano) :
            await _indicadores.DoAtendimentosAsync(s => s.Data!.Value.Year == ano && s.Setor == setor);


        string? _atmonth = string.Empty;
        foreach (var m in LReports.AtendimentosMonth!)
            _atmonth += string.Format(@"{{x:`{0}`,y:{1}}},", m.Key, m.Value);

        string? _svmonth = string.Empty;
        foreach (var m in LReports.ServicesMonth!)
            _svmonth += string.Format(@"{{x:`{0}`,y:{1}}},", m.Key, m.Value);

        string[]? _perfil = new string[2];
        foreach (var p in LReports.PerfilAtendimento!)
        {
            _perfil[0] += string.Format(@"{0},", p.Value);
            _perfil[1] += string.Format(@"`{0}`,", p.Key.NormalizeText());
        }

        string? _attimeday = string.Empty;
        foreach (var (timeday, valor) in LReports.TimeDay!)
            _attimeday += string.Format(@"{{x:`{0}H`,y:{1}}},", timeday, valor);

        string? _svtimeday = string.Empty;
        foreach (var (timeday, valor) in LReports.ServTimeDay!)
            _svtimeday += string.Format(@"{{x:`{0}H`,y:{1}}},", timeday, valor);

        string[]? _canal = new string[2];
        foreach (var (canal, valor, percent) in LReports.Canais!)
        {
            _canal[0] += string.Format(@"{0},", valor);
            _canal[1] += string.Format(@"`{0}`,", canal);
        }

        string[]? _atdistintos = new string[2];
        foreach (var x in LReports.PerfilCliente!)
        {
            _atdistintos[0] += string.Format(@"{0},", x.Value);
            _atdistintos[1] += string.Format(@"`{0}`,", x.Key);
        }

        string? _cli_distintos = string.Empty;
        foreach (var x in LReports.Clientes!)
            _cli_distintos += string.Format(@"{{x:`{0}`,y:{1}}},", x.Key.NormalizeText(), x.Value);

        string? _emp_distintas = string.Empty;
        foreach (var x in LReports.Empresas!)
            _emp_distintas += string.Format(@"{{x:`{0}`,y:{1}}},", x.Key.NormalizeText(), x.Value);

        string? _faixa_etaria = string.Empty;
        foreach (var x in LReports.FaixaEtariaCliente!)
            _faixa_etaria += string.Format(@"{{x:`{0}`,y:{1}}},", x.Key.NormalizeText(), x.Value);

        string[]? _cli_genero = new string[2];
        foreach (var x in LReports.GeneroCliente!)
        {
            _cli_genero[0] += string.Format(@"{0},", x.Value);
            _cli_genero[1] += string.Format(@"`{0}`,", x.Key);
        }

        string? _emp_faixa_etaria = string.Empty;
        foreach (var x in LReports.EmpresasIdade!)
            _emp_faixa_etaria += string.Format(@"{{x:`{0}`,y:{1}}},", x.faixa.NormalizeText(), x.valor);

        string[]? _segmento = new string[2];
        foreach (var x in LReports.EmpresasSetores!)
        {
            _segmento[0] += string.Format(@"{0},", x.valor);
            _segmento[1] += string.Format(@"`{0}`,", x.setor);
        }

        AtTimeDay = _attimeday.Length > 0 ? _attimeday[..^1] : _attimeday;
        SvTimeDay = _svtimeday.Length > 0 ? _svtimeday[..^1] : _svtimeday;
        Perfil = _perfil;
        Canais = _canal;
        AtendimentosMonth = _atmonth.Length > 0 ? _atmonth[..^1] : _atmonth;
        ServicosMonth = _svmonth.Length > 0 ? _svmonth[..^1] : _svmonth;
        AtendimentosDistintos = _atdistintos;
        ClientesDistintos = _cli_distintos.Length > 0 ? _cli_distintos[..^1] : _cli_distintos;
        EmpresasDistintas = _emp_distintas.Length > 0 ? _emp_distintas[..^1] : _emp_distintas;
        FaixaEtaria = _faixa_etaria.Length > 0 ? _faixa_etaria[..^1] : _faixa_etaria;
        Genero = _cli_genero;
        FaixaEtariaEmpresas = _emp_faixa_etaria.Length > 0 ? _emp_faixa_etaria[..^1] : _emp_faixa_etaria;
        Segmentos = _segmento;
    }

    private async Task<IEnumerable<string>> Setores()
    {
        var _dominioID = HttpContext.Session.GetString("DominioID");
        var _org = await _organizacao.GetAsync(Guid.Parse(_dominioID!));
        return from st in await _organizacao.DoListAsync(s => s.Dominio == _org.Id)
               select st.Acronimo;
    }
}
