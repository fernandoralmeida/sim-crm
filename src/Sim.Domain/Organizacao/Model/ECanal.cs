
namespace Sim.Domain.Organizacao.Model
{
    public  class ECanal
    {
        public ECanal()
        {

        }
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public EOrganizacao? Dominio { get; set; } //Secretaria
        public bool Ativo { get; set; }
    }
}
