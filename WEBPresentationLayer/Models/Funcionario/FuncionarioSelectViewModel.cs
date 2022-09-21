using Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace WEBPresentationLayer.Models.Funcionario
{
    public class FuncionarioSelectViewModel
    {
        [Display(Name = "ID do Funcionario")]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data Nascimento")]
        public DateTime DataNascimento { get; set; }

        [Display(Name = "CPF")]
        public string CPF { get; set; }

        [Display(Name = "RG")]
        public string RG { get; set; }
        public Genero Genero { get; set; }

        [Display(Name = "Nivel De Acesso")]
        public NivelDeAcesso NivelDeAcesso { get; set; }
    }
}