using Sim.Domain.Organizacao.Model;

namespace Sim.Domain.Evento.Model
{
    public class EParceiro
    {
        public EParceiro()
        {  }

        public Guid Id { get; set; }
        public string? Nome { get; set; } //nome do tipo
        public EOrganizacao? Dominio { get; set; } //Tipo
        public bool Ativo { get; set; }
    }
}
