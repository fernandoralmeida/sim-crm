using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Sim.Domain.Organizacao.Model;

namespace Sim.Application.VM
{
    public class VMServicos {
        public VMServicos() { }
        
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        [DisplayName("Serviço")]
        public string? Nome { get; set; }
        
        [Required]
        [DisplayName("Dominio")]
        public EOrganizacao? Dominio { get; set; } //Secretaria
        
        [DisplayName("Ativo")]
        public bool? Ativo { get; set; }
    }    
}