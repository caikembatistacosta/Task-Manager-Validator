using System.ComponentModel.DataAnnotations;
namespace WEBPresentationLayer.Models.Cliente
{
    public class ClienteUpdateViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "O nome deve ser informado.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "O nome deve conter entre 3 e 30 caracteres.")]
        public string Nome { get; set; }


        [Display(Name = "Email")]
        public string Email { get; set; }


        public bool IsAtivo { get; set; }
    }
}
