using System.ComponentModel.DataAnnotations;

namespace WEBPresentationLayer.Models.Demanda
{
    public class DemandaInsertViewModel
    {

        [Required(ErrorMessage = "O nome deve ser informado.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "O nome deve conter entre 3 e 30 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A Descrição deve ser informada.")]
        [Display(Name = "Descrição Curta")]
        public string DescricaoCurta { get; set; }
        [Required(ErrorMessage = "A Descrição deve ser informada.")]
        [Display(Name = "Descrição Detalhada")]
        public string DescricaoDetalhada { get; set; }

        [Required(ErrorMessage = "A data deve ser informada.")]
        [DataType(DataType.Date)]
        public DateTime DataInicio { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "A data do final da demanda deve ser informado.")]
        public DateTime DataFim { get; set; }


    }

}
