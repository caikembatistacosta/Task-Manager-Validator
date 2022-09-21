using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class DataResponseFactory<T>
    {
        public static DataResponse<T> CreateSuccessDataResponse(List<T> itens)
        {
            return new DataResponse<T>()
            {
                HasSuccess = true,
                Message = "Operação realizada com sucesso",
                Data = itens,
            };
        }
        public static DataResponse<T> CreateFailureDataResponse(Exception ex)
        {
            return new DataResponse<T>()
            {
                HasSuccess = false,
                Message = "Operação falhou",
                Exception = ex,
            };
        }
    }
}
