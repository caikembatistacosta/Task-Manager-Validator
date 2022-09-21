using System.ComponentModel.DataAnnotations;
namespace WebApi.Models.Demanda
{
    public class DemandaUpdateViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "O nome deve ser informado.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "O nome deve conter entre 3 e 30 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A Descrição deve ser informada.")]
        [Display(Name = "Descrição")]
        public string DescricaoCurta { get; set; }
        [Required(ErrorMessage = "A Descrição deve ser informada.")]
        [Display(Name = "Descrição")]
        public string DescricaoDetalhada { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataFim { get; set; }
    }
}
