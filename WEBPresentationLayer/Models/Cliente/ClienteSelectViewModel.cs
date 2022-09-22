using Entities;
using Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace WEBPresentationLayer.Models.Cliente
{
    public class ClienteSelectViewModel
    {
        public int ID { get; set; }
        public string Nome { get; set; }

        public string CPF { get; set; }

        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
        //public string CEP { get; set; }
        //public string Rua { get; set; }
        //public string Bairro { get; set; }
        //public string Numero { get; set; }
        //public string Cidade { get; set; }
        //public string Estado { get; set; }
        public Endereco Endereco { get; set; }
    }
}
