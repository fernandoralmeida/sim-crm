using Sim.Domain.BancoPovo.Models;
using Sim.Domain.BancoPovo.Interfaces;
using Sim.Application.BancoPovo.Interfaces;
using Sim.Domain.Helpers;
using System.Linq.Expressions;
using System.Globalization;

namespace Sim.Application.BancoPovo.Services;
public class AppServiceContrato : AppServiceBase<EContrato>, IAppServiceContratos
{
    private readonly IServiceContratos _contrato;
    public AppServiceContrato(IServiceContratos serviceBase) : base(serviceBase)
    {
        _contrato = serviceBase;
    }

    public async Task<IEnumerable<EContrato>?> DoListAsync(Expression<Func<EContrato, bool>>? filter = null)
    {
        return await _contrato.DoListAsync(filter);
    }

    public async Task<EReports?> DoReportsAsync(IEnumerable<EContrato> lista)
    {
        return await Task.Run(() =>
        {
            var _ret = new EReports();
            var _ec = new EContrato();

            _ret.ContratosReprovados = new KeyValuePair<string, int>
                                        ("Reprovados", lista.Where(s => EContrato.ContratosReprovados(s)).Count());

            _ret.ContratosLiquidados = new KeyValuePair<string, int>
                                        ("Liquidados", lista.Where(s => EContrato.ContratosLiquidados(s)).Count());

            _ret.ContratosCancelados = new KeyValuePair<string, int>
                                        ("Cancelados", lista.Where(s => EContrato.ContratosCancelados(s)).Count());

            _ret.ContratosRenegociados = new KeyValuePair<string, int>
                                        ("Renegociados", lista.Where(s => EContrato.ContratosRenegociados(s)).Count());

            _ret.ContratosEmAnalise = new KeyValuePair<string, int>
                                        ("Em Análise", lista.Where(s => EContrato.ContratosEmAnalise(s)).Count());

            _ret.ContratosAprovadosInadimplente = new KeyValuePair<string, int>
                                        ("Inadimplente", lista.Where(s => EContrato.ContratosAprovadosInadimplente(s)).Count());

            _ret.ContratosAprovadosRegulares = new KeyValuePair<string, int>
                                        ("Regulares", lista.Where(s => EContrato.ContratosAprovadosRegulares(s)).Count());

            _ret.ValorContratosAnalise = string.Format("{0} {1}", NumberFormatInfo.CurrentInfo.CurrencySymbol, EContrato.ValorContratosAnalise(lista));
            _ret.ValorContratosInadimplentes = string.Format("{0} {1}", NumberFormatInfo.CurrentInfo.CurrencySymbol, EContrato.ValorContratosInadimplentes(lista));
            _ret.ValorContratosRegulares = string.Format("{0} {1}", NumberFormatInfo.CurrentInfo.CurrencySymbol, EContrato.ValorContratosRegulares(lista));
            _ret.ValorContratosRenegociados = string.Format("{0} {1}", NumberFormatInfo.CurrentInfo.CurrencySymbol, EContrato.ValorContratosRenegociados(lista));
            _ret.ValorMedio = $"{NumberFormatInfo.CurrentInfo.CurrencySymbol} {EContrato.ValorMedio(lista):N2}";
            _ret.TaxaInadimplencia = string.Format("{0} %", EContrato.TaxaInadimplencia(lista).ToString("N0"));

            var _setores = new List<string>();
            var _r_setores = new List<(string, int, float)>();
            var _faixas = new List<(string, int, float)>();
            var _locais = new List<string>();
            var _r_locais = new List<(string, int, float)>();

            foreach (var item in lista)
            {
                if (item.Empresa == null)
                {
                    _setores.Add("PF");
                    _locais.Add(item.Cliente!.Bairro!);
                }
                else
                {
                    string _cnae = item.Empresa.CNAE_Principal!.Remove(2, 8);
                    if (_cnae.All(char.IsDigit))
                        _setores.Add(Convert.ToInt32(_cnae).DoSetores());

                    _locais.Add(item.Empresa.Bairro!);
                }
            }

            var _t_lista = Convert.ToSingle(lista.Count());

            foreach (var item in _setores.GroupBy(s => s).OrderByDescending(s => s.Count()))
                _r_setores.Add((item.Key, item.Count(), (item.Count() / _t_lista) * 100F));

            _ret.Setores = _r_setores;

            var _5000 = lista.Where(s => s.Valor <= 5000M).Count();
            _faixas.Add(("Até 5.000", _5000, (_5000 / _t_lista) * 100F));

            var _10000 = lista.Where(s => s.Valor > 5000M && s.Valor <= 10000M).Count();
            _faixas.Add(("5.000 - 10.000", _10000, (_10000 / _t_lista) * 100F));

            var _15000 = lista.Where(s => s.Valor > 10000M && s.Valor <= 15000M).Count();
            _faixas.Add(("10.000 - 15.000", _15000, (_15000 / _t_lista) * 100F));

            var _21000 = lista.Where(s => s.Valor > 15000M && s.Valor <= 21000M).Count();
            _faixas.Add(("15.000 - 21.000", _21000, (_21000 / _t_lista) * 100F));

            _ret.Faixas = _faixas;

            foreach (var item in _locais.GroupBy(s => s).OrderByDescending(s => s.Count()).Take(5))
                _r_locais.Add((item.Key, item.Count(), (item.Count() / _t_lista) * 100F));

            _ret.Locais = _r_locais;

            _ret.ListaContratos = lista;

            return _ret;
        });
    }

    public async Task<EContrato?> GetIdAsync(Guid id)
    {
        return await _contrato.GetIdAsync(id);
    }
}