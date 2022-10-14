using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Constants
{
    internal class DemandaConstants
    {
        public const string MENSAGEM_ERRO_DESCRICAO_CURTA_MENOR = "Descricção curta deve ter mais de 5 caracteres";
        public const string MENSAGEM_ERRO_DESCRICAO_CURTA_MAIOR = "Descrição curta deve ter no máximo 30 caracteres";
        public const string MENSAGEM_ERRO_DESCRICAO_DETALHADA_MENOR = "Descrição detalhada deve ter mais de 10 caracteres";
        public const string MENSAGEM_ERRO_DESCRICAO_DETALHADA_MAIOR = "Descrição detalhada deve ter menos de 100 caracteres";
    }
}
