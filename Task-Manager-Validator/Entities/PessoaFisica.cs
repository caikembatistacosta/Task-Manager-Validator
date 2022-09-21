using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PessoaFisica
    {
        public string CPF { get; set; }
        public string RG { get; set; }

        public DateTime DataNascimento { get; set; }
        public int Idade
        {
            get
            {
                DateTime today = DateTime.Today;
                int age = today.Year - DataNascimento.Year;
                if (DataNascimento.Date > today.AddYears(-age))
                {
                    age--;
                }
                return age;

            }

        }

    }
}