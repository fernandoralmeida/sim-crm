using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Sim.Domain.Entity;
using Sim.Domain.Organizacao.Model;

namespace Sim.UI.Web.Pages.Atendimento
{

    public class InputModelAtendimento
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        [DisplayName("Protocolo")]
        public string? Protocolo { get; set; }

        [DisplayName("Data")]
        [DataType(DataType.Date)]
        public DateTime? Data { get; set; }

        [DisplayName("Fim")]
        [DataType(DataType.Time)]
        public DateTime? DataF { get; set; }

        [Required(ErrorMessage = "Selecione o setor do atendimento!")]
        [DisplayName("Setor")]
        public string? Setor { get; set; }

        [Required(ErrorMessage = "Selecione o canal do atendimento!")]
        [DisplayName("Canal do Atendimento")]
        public string? Canal { get; set; }

        [Required(ErrorMessage = "Adicione um serviço ou mais!")]
        [DisplayName("Serviços")]
        public string? Servicos { get; set; }

        //[Required]
        [DisplayName("Descrição do Atendimento")]
        public string? Descricao { get; set; }

        [DisplayName("Status")]
        public string? Status { get; set; }
        public DateTime? Ultima_Alteracao { get; set; }
        public bool Ativo { get; set; }
        public string? Owner_AppUser_Id { get; set; }

        [DisplayName("Pessoa")]
        public virtual Pessoa? Pessoa { get; set; }

        [DisplayName("Empresa")]
        public virtual Empresas? Empresa { get; set; }

        [DisplayName("Dominio")]
        public virtual EOrganizacao? Dominio { get; set; }

    }
}
