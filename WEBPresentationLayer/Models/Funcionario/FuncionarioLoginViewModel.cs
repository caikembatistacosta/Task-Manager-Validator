using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace WEBPresentationLayer.Models.Funcionario
{
    public class FuncionarioLoginViewModel
    {
        [Required(ErrorMessage = "Email deve ser informado")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage ="Senha deve ser informada")]
        [StringLength(20,MinimumLength = 6, ErrorMessage = "Senha deve conter entr 6 a 20 caracteres")]
        [JsonIgnore]
        public string Senha { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}
