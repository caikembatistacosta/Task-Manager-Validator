using BLL.Validators.Funcionarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validators.Funcionarios
{
    internal class FuncInsertValidator : FuncionarioValidator
    {
        public FuncInsertValidator()
        {
            base.ValidateNome();
            base.ValidateCPF();
            base.ValidateEmail();
            base.ValidateSenha();
            base.ValidateDataNascimento();
            base.ValidateIdade();

        }
    }
}