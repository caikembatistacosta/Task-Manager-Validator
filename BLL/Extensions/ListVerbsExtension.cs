using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Extensions
{
    internal class ListVerbsExtension
    {
        public List<string> Lista { get; set; }
        public ListVerbsExtension()
        {
            Lista = new List<string>
            {
                "Get",
                "Set",
            };
        }
        
    }
}
