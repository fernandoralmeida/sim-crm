using Sim.Domain.Organizacao.Model;
namespace Sim.Domain.Evento.Model
{
    public class ETipo
    {
        public ETipo()
        { }
        public Guid Id { get; set; }
        public string? Nome { get; set; } 
        public string? Tipo { get; set; } 
        public EOrganizacao? Dominio { get; set; } 
        public bool Ativo { get; set; }
    }
}
