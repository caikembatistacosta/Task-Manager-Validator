using System.ComponentModel.DataAnnotations;

namespace WEBPresentationLayer.Models.Cliente
{
    public class ClienteInsertViewModel
    {
        [Required(ErrorMessage = "O nome deve ser informado.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "O nome deve conter entre 3 e 30 caracteres.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "A CPF deve ser informada.")]
        [Display(Name = "CPF")]
        public string CPF { get; set; }
        [Required(ErrorMessage = "A CPF deve ser informada.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        public string CEP { get; set; }
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public string Numero { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

    }
}
