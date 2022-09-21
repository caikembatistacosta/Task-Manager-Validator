using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Endereco : Entity
    {

        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string Numero { get; set; }
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string PontoReferencia { get; set; }
        public Estado Estado { get; set; }
        public int EstadoId { get; set; }

        public Cliente Cliente { get; set; }
    }
}
