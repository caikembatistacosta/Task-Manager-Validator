using System.ComponentModel.DataAnnotations;

namespace WEBPresentationLayer.Models.Funcionario
{
    public class FuncionarioUpdateSenhaViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Informe a Senha")]
        [DataType(DataType.Password)]

        public string Senha { get; set; }
        [Required(ErrorMessage = "Informe nova a Senha")]
        [Display(Name = "Nova Senha")]
        [DataType(DataType.Password)]
        public string NovaSenha { get; set; }
        [Required(ErrorMessage = "as senhas devem ser iguais Senha")]
        [Compare("NovaSenha", ErrorMessage = "As senha devem bater")]
        [Display(Name = "Confirmar Nova Senha")]
        [DataType(DataType.Password)]
        public string NovaSenhaConfirmar { get; set; }
    }
}
