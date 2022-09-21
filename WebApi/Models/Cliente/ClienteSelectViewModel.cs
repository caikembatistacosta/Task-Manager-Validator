using Entities;
using Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Cliente
{
    public class ClienteSelectViewModel
    {
        public int ID { get; set; }
        public string Nome { get; set; }

        public string CPF { get; set; }

        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
        public Endereco Endereco { get; set; }
    }
}
