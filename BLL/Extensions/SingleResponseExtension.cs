using Shared;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Extensions
{
    internal static class SingleResponseExtension
    {
        /// <summary>
        /// Converte a validação para um SingleResponse.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static SingleResponse<T> ConvertToSingleResponse<T>(this ValidationResult result, T item)
        {
            SingleResponse<T> singleResponse = new();
            if (result.IsValid)
            {
                singleResponse.HasSuccess = true;
                singleResponse.Message = "Operação efetuada com sucesso.";
                singleResponse.Item = item;
                return singleResponse;
            }

            StringBuilder builder = new StringBuilder();
            foreach (ValidationFailure fail in result.Errors)
            {
                builder.AppendLine(fail.ErrorMessage);
            }

            singleResponse.HasSuccess = false;
            singleResponse.Message = builder.ToString();
            return singleResponse;
        }
    }
}
