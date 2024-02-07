using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Sim.UI.Web.Pages.Planner
{    
    public class InputModelPlanner
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        [DisplayName("SEGUNDA")]
        public string? Segunda { get; set; }

        [DisplayName("TERÇA")]
        public string? Terca { get; set; }

        [DisplayName("QUARTA")]
        public string? Quarta { get; set; }

        [DisplayName("QUINTA")]
        public string? Quinta { get; set; }

        [DisplayName("SEXTA")]
        public string? Sexta { get; set; }

        [DisplayName("SÁBADO")]
        public string? Sabado { get; set; }

        [DisplayName("SEMANA QUE VEM")]
        public string? ProximaSemana { get; set; }

        [DisplayName("PRIORIDADES")]
        public string? Prioridades { get; set; }

        [DisplayName("ANOTAÇÃO")]
        public string? Anotacao { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DataInicial { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DataFinal { get; set; }

        [DisplayName("Operador")]
        public string? Owner_AppUser_Id { get; set; }

        [DisplayName("Registro Ativo")]
        public bool Ativo { get; set; }
    }
}
