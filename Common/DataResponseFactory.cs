using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class DataResponseFactory<T>
    {
        private static DataResponseFactory<T> _factory;

        public static DataResponseFactory<T> CreateInstance()
        {
            _factory ??= new DataResponseFactory<T>();
            return _factory;
        }
        public DataResponse<T> CreateSuccessDataResponse(List<T> itens)
        {
            return new DataResponse<T>()
            {
                HasSuccess = true,
                Message = "Operação realizada com sucesso",
                Data = itens,
            };
        }
        public DataResponse<T> CreateFailureDataResponse(Exception ex)
        {
            return new DataResponse<T>()
            {
                HasSuccess = false,
                Message = "Operação falhou",
                Exception = ex,
            };
        }
        public DataResponse<T> CreateFailureDataResponse()
        {
            return new DataResponse<T>()
            {
                HasSuccess = false,
                Message = "Operação falhou",
            };
        }
    }
}
