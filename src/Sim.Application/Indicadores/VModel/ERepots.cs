namespace Sim.Application.Indicadores.VModel;
public class EReports {
    public KeyValuePair<string, int> Atendimentos { get; set; }
    public KeyValuePair<string, int> Serviços { get; set; }
    public KeyValuePair<string, int> Eventos { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? AtendimentosMonth { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? ServicesMonth { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? EventosMonth { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? ListaServicos { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? PerfilAtendimento { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? PerfilCliente { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? FaixaEtariaCliente { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? GeneroCliente { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? Clientes { get; set; }
    public IEnumerable<KeyValuePair<string, int>>? Empresas { get; set; }
    public IEnumerable<(string setor, int valor, float percent)>? EmpresasSetores { get; set; }
    public IEnumerable<(string zone, int valor, float percent)>? EmpresasLocation { get; set; }
    public IEnumerable<(string faixa, int valor, float percent)>? EmpresasIdade { get; set; }
    public IEnumerable<(string fiscal, int valor, float percent)>? EmpresasRegimeFiscal { get; set; }
 }