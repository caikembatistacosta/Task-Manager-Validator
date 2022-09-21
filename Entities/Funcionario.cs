using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Rg { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public Genero Genero { get; set; }
        public NivelDeAcesso NivelDeAcesso { get; set; }
        public bool IsAtivo { get; set; }
        public string? RefreshToken { get; set; }
        [JsonIgnore]
        public string Senha { get; set; }
    }
}
