using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Demanda : Entity
    {

        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string Nome { get; set; }
        public string DescricaoCurta { get; set; }
        public string DescricaoDetalhada { get; set; }
        public StatusDemanda StatusDaDemanda { get; set; }
    }
}
