using Entities;

namespace WebApi.Models.Cliente
{
    public class ClienteDetailsViewModel
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set;}
        public string Cpf { get; set; }
        public Endereco Endereco{ get; set; }
        public bool IsAtivo { get; set; }
    }
}
