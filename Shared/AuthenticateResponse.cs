using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class AuthenticateResponse : Response
    {
        public string Token { get; set; }
        public string Refresh { get; set; }
    }
}
