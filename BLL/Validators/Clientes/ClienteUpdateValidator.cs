using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validators.Clientes
{
    internal class ClienteUpdateValidator : ClienteValidator
    {
        public ClienteUpdateValidator()
        {
            base.ValidateEmail();
        }
    }
}
