using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Funcionario 
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public Genero Genero { get; set; }
        public NivelDeAcesso NivelDeAcesso { get; set; }
        public bool IsAtivo { get; set; }
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