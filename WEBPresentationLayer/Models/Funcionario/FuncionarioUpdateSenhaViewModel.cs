using System.ComponentModel.DataAnnotations;

namespace WEBPresentationLayer.Models.Funcionario
{
    public class FuncionarioUpdateSenhaViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Informe a Senha")]
        [DataType(DataType.Password)]

        public string Senha { get; set; }

        [Display(Name = "Nova Senha")]
        [DataType(DataType.Password)]
        public string NovaSenha { get; set; }

        [Compare("NovaSenha", ErrorMessage = "As senha devem bater")]
        [Display(Name = "Confirmar Nova Senha")]
        [DataType(DataType.Password)]
        public string NovaSenhaConfirmar { get; set; }
    }
}
