using Common;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Extensions
{
    internal static class SingleResponseExtension
    {
        public static SingleResponse<T> ConvertToResponse<T>(this ValidationResult result, T item)
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
