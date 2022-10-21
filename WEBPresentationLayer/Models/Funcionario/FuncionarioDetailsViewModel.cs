using Entities;
using Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WEBPresentationLayer.Models.Funcionario
{
    public class FuncionarioDetailsViewModel
    {

        [Display(Name = "ID")]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }

        public string Email { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Nascimento")]
        public DateTime DataNascimento { get; set; }


        public string CPF { get; set; }


        public string RG { get; set; }
        public Genero Genero { get; set; }


        [Display(Name = "Nivel De Acesso")]
        public NivelDeAcesso NivelDeAcesso { get; set; }
        public Endereco Endereco { get; set; }
    }
}
