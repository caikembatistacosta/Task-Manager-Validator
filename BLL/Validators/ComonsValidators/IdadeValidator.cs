using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace BLL.Validators.ComonsValidators
{
    internal static class IdadeValidator
    {
        public static IRuleBuilderOptions<PessoaFisica, int> IsIdadeValid<PessoaFisica>(this IRuleBuilder<PessoaFisica, int> param)
        {
            return param.Must(c => ValidarIdade(c));
        }
        public static bool ValidarIdade(int idade)
        {
            if (idade < 14 || idade > 120)
                return false;
            else
                return true;
        }
    }
}