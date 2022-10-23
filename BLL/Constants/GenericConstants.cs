using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Constants
{
    internal static class GenericConstants
    {
        public const string MENSAGEM_ERRO_ID_VAZIO = "ID deve ser informado.";
        public const string MENSAGEM_ERRO_NOME_VAZIO = "Nome deve ser informado";
        public const string MENSAGEM_ERRO_NOME_CURTO = "O nome deve conter no minímo 3 caracteres";
        public const string MENSAGEM_ERRO_NOME_GRANDE = "O nome deve conter no máximo 30 caracteres";
    }
}
