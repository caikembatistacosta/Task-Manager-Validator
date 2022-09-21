using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validators.Demandas
{
    internal class DemandaInsertValidator : DemandaValidator
    {
        public DemandaInsertValidator()
        {
            base.ValidateNome();
            base.ValidateDescricaoCurta();
            base.ValidateDescricaoDetalhada();
        }
    }
}
