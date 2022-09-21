using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Constants
{
    internal static class ClienteConstants
    {
        public const string MENSAGEM_ERRO_NOME_CURTO = "O nome do cliente deve conter no minímo 3 caracteres";
        public const string MENSAGEM_ERRO_NOME_GRANDE = "O nome do cliente deve conter no máximo 30 caracteres";
    }
}
