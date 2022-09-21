using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Cliente
    {
        public Cliente()
        {
            this.Endereco = new Endereco();
        }
        public int ID { get; set; }
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
