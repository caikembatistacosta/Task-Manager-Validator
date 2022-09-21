using Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace WEBPresentationLayer.Models.Funcionario
{
    public class FuncionariosInsertViewModel
    {
        [Required(ErrorMessage = "Nome deve ser informado.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Nome deve conter entre 3 e 30 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Sobrenome deve ser informado!")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Sobrenome deve conter entre 3 e 30 caracteres.")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "Email deve ser informado.")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a Senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }


        [Required(ErrorMessage = "Data Nascimento deve ser informada")]
        [DataType(DataType.Date)]
        [Display(Name = "Data Nascimento")]
        public DateTime DataNascimento { get; set; }


        [Required(ErrorMessage = "CPF deve ser informado.")]
        [Display(Name = "CPF")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "RG deve ser informado.")]
        [Display(Name = "RG")]
        public string RG { get; set; }


        [Required(ErrorMessage = "Genero deve ser informado.")]

        public Genero Genero { get; set; }

        [Required(ErrorMessage = "Informar nivel de acesso")]
        [Display(Name = "Nivel De Acesso")]
        public NivelDeAcesso NivelDeAcesso { get; set; }

    }


}
