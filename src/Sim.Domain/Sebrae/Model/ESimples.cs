using Sim.Domain.Entity;

namespace Sim.Domain.Sebrae.Model;

public class ESimples
{
    public Guid Id { get; set; }
    public string? Documento { get; set; }
    public string? Exercicio { get; set; }
    public string? Chave { get; set; }
    public Empresas? Empresa { get; set; }
}