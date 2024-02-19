using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Sim.Domain.Evento.Model;
using Sim.Domain.Organizacao.Model;

namespace Sim.UI.Web.Pages.Agenda
{
    public class InputModelEvento
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        [Required]
        [DisplayName("Código")]
        public int Codigo { get; set; }

        [Required]
        public string? Tipo { get; set; }

        [Required]
        public string? Nome { get; set; }

        [Required]
        [DisplayName("Formato")]
        public string? Formato { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? Data { get; set; }

        [DisplayName("Descrição")]
        public string? Descricao { get; set; }

        [Required]
        [DisplayName("Setor Responsável")]
        public string? Owner { get; set; }

        [DisplayName("Parceiro")]
        public string? Parceiro { get; set; }

        [DisplayName("Lotação")]
        public int Lotacao { get; set; }

        [DisplayName("Situação")]
        public EEvento.ESituacao Situacao { get; set; }

        public EOrganizacao? Dominio { get; set; }

        public virtual ICollection<InputModelInscricao>? Inscritos { get; set; }
    }
}
