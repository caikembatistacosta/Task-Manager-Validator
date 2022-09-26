using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Funcionario
{
    public class FuncionarioForgotMyPasswordViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage ="Email deve ser informado")]
        public string Email { get; set; }
    }
}
