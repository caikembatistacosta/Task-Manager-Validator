using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class DataResponseFactory<T>
    {
        private static DataResponseFactory<T> _factory;

        /// <summary>
        /// Cria uma instancia static de um DataResponseFactory para ser usado no sistema todo.
        /// </summary>
        /// <returns></returns>
        public static DataResponseFactory<T> CreateInstance()
        {
            _factory ??= new DataResponseFactory<T>();
            return _factory;
        }
        /// <summary>
        /// É um método que retorna sucesso no DataResponse.
        /// </summary>
        /// <param name="itens"></param>
        /// <returns></returns>
        public DataResponse<T> CreateSuccessDataResponse(List<T> itens)
        {
            return new DataResponse<T>()
            {
                HasSuccess = true,
                Message = "Operação realizada com sucesso",
                Data = itens,
            };
        }
        /// <summary>
        /// É um método que retorna falso no DataResponse.
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public DataResponse<T> CreateFailureDataResponse(Exception ex)
        {
            return new DataResponse<T>()
            {
                HasSuccess = false,
                Message = "Operação falhou",
                Exception = ex,
            };
        }
        /// <summary>
        /// É um método que retorna falso no DataResponse.
        /// </summary>
        /// <returns></returns>
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
