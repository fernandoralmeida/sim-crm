namespace Sim.Application.Sebrae.Views;

public record RExportRAE
{
    public int Count { get; set; }
    public string? Data { get; set; }
    public string? Cliente { get; set; }
    public string? Servicos { get; set; }
    public string? Canal { get; set; }
    public string? Lancamento { get; set; }
    public string? NumeroRAE { get; set; }
    public string? Atendente { get; set; }
}