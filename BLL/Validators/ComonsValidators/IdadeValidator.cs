using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace BusinessLogicalLayer.Validators.ComonsValidators
{
    internal static class IdadeValidator
    {
        /// <summary>
        /// Um método de extensão para validar a idade do usuário.
        /// </summary>
        /// <typeparam name="PessoaFisica"></typeparam>
        /// <param name="param"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<PessoaFisica, int> IsIdadeValid<PessoaFisica>(this IRuleBuilder<PessoaFisica, int> param)
        {
            return param.Must(c => ValidarIdade(c));
        }
        /// <summary>
        /// Valida a idade.
        /// </summary>
        /// <param name="idade"></param>
        /// <returns></returns>
        public static bool ValidarIdade(int idade)
        {
            if (idade < 14 || idade > 120)
                return false;
            else
                return true;
        }
    }
}