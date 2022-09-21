using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class SingleResponseFactory<T>
    {
        public static SingleResponse<T> CreateSuccessSingleResponse(T item)
        {
            return new SingleResponse<T>()
            {
                HasSuccess = true,
                Message = "Operação realizada com sucesso",
                Item = item,
            };
        }
        public static SingleResponse<T> CreateFailureSingleResponse(Exception ex)
        {
            return new SingleResponse<T>()
            {
                HasSuccess = false,
                Message = "Operação falhou",
                Exception = ex,
            };

        }
        public static SingleResponse<T> CreateFaiulureSingleResponse()
        {
            return new SingleResponse<T>()
            {
                HasSuccess = false,
                Message = "Operação falhou",
            };
        }
    }
}
