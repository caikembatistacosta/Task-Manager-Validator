using System.ComponentModel.DataAnnotations;

namespace WEBPresentationLayer.Models.Funcionario
{
    public class FuncionarioForgotMyPasswordViewModel
    {
        public int ID { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email deve ser informado")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Senha deve ser informada")]
        //public string Senha { get; set; }
        //[DataType(DataType.Password)]
        //[Required(ErrorMessage = "A nova senha deve ser informada")]
        //public string NovaSenha { get; set; }
    }
}
