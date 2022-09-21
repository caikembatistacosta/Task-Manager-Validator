using Common;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Extensions
{
    internal static class DataResponseExtension
    {
        public static DataResponse<T> ConvertToResponse<T>(this ValidationResult result, List<T> itens)
        {
            DataResponse<T> dataResponse = new();
            if (result.IsValid)
            {
                dataResponse.HasSuccess = true;
                dataResponse.Message = "Operação efetuada com sucesso.";
                dataResponse.Data = itens;
                return dataResponse;
            }

            StringBuilder builder = new StringBuilder();
            foreach (ValidationFailure fail in result.Errors)
            {
                builder.AppendLine(fail.ErrorMessage);
            }

            dataResponse.HasSuccess = false;
            dataResponse.Message = builder.ToString();
            return dataResponse;
        }
    }
}
