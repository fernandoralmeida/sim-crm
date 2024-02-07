using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sim.UI.Web.Pages.Cliente
{
    public class InputModelPessoa
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        // Pessoal
        [Required(ErrorMessage = "Preencha campo Nome")]
        public string? Nome { get; set; }

        [DisplayName("Nome Social")]
        public string? Nome_Social { get; set; }

        [Required(ErrorMessage = "Preencha data de nascimento")]
        [DisplayName("Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime? Data_Nascimento { get; set; }

        [Required(ErrorMessage = "Preencha o campo CPF")]
        public string? CPF { get; set; }

        public string? RG { get; set; }

        [DisplayName("Emissor")]
        public string? RG_Emissor { get; set; }

        [DisplayName("UF")]
        public string? RG_Emissor_UF { get; set; }

        [Required(ErrorMessage = "Preencha o campo gênero")]
        [DisplayName("Gênero")]
        public string? Genero { get; set; }

        public string? Deficiencia { get; set; }

        [DisplayName("Física")]
        public bool Fisica { get; set; }

        [DisplayName("Visual")]
        public bool Visual { get; set; }

        [DisplayName("Auditiva")]
        public bool Auditiva { get; set; }

        [DisplayName("Intelectual")]
        public bool Intelectual { get; set; }

        //Correspondencia
        public string? CEP { get; set; }
        public string? Logradouro { get; set; }
        public string? Numero { get; set; }
        public string? Complemento { get; set; }
        public string? Bairro { get; set; }
        public string? Cidade { get; set; }
        public string? UF { get; set; }

        //Contato
        //public List<KeyValuePair<string, string>> Telefones { get; set; }
        [DisplayName("Telefone Móvel")]
        public string? Tel_Movel { get; set; }

        [DisplayName("Telefone Fixo")]
        public string? Tel_Fixo { get; set; }

        [DisplayName("E-mail")]
        public string? Email { get; set; }

        //Informacao Cadastro
        [DisplayName("Data de Cadastro")]
        public DateTime? Data_Cadastro { get; set; }

        [DisplayName("Ultima Alteração")]
        public DateTime? Ultima_Alteracao { get; set; }

        [DisplayName("Cadastro")]
        public bool Ativo { get; set; }
    }
}
