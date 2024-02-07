
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Sim.Domain.Evento.Model;
using Sim.Domain.Entity;

namespace Sim.UI.Web.Pages.Agenda
{
    public class InputModelInscricao
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        public int Numero { get; set; }

        public string? AplicationUser_Id { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? Data_Inscricao { get; set; }

        public bool Presente { get; set; }

        public virtual Pessoa? Participante { get; set; }
        public virtual Empresas? Empresa { get; set; }
        public virtual EEvento? Evento { get; set; }
    }
}
