using Entities.Enums;

namespace Entities
{
    public class Cliente : Entity
    {
               
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public Genero Genero { get; set; }
        public bool EstaAtivo { get; set; }
        public int EnderecoID { get; set; }
        public Endereco Endereco { get; set; }
    }
}
