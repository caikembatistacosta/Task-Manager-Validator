using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class SingleResponseFactory<T>
    {
        private static SingleResponseFactory<T> _factory;

        /// <summary>
        /// Cria uma instância static do SingleResponseFactory.
        /// </summary>
        /// <returns></returns>
        public static SingleResponseFactory<T> CreateInstance()
        {
            _factory ??= new SingleResponseFactory<T>();
            return _factory;
        }
        /// <summary>
        /// É um método que retorna sucesso no SingleResponse.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public SingleResponse<T> CreateSuccessSingleResponse(T item)
        {
            return new SingleResponse<T>()
            {
                HasSuccess = true,
                Message = "Operação realizada com sucesso",
                Item = item,
            };
        }
        /// <summary>
        /// É um método que retorna sucesso no SingleResponse.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public SingleResponse<T> CreateSuccessSingleResponse(T item, string message)
        {
            return new SingleResponse<T>()
            {
                HasSuccess = true,
                Message = "Operação realizada com sucesso",
                Item = item,
            };
        }
        /// <summary>
        /// É um método que retorna falso no SingleResponse.
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public SingleResponse<T> CreateFailureSingleResponse(Exception ex) => new()
        {
            HasSuccess = false,
            Message = "Operação falhou",
            Exception = ex,
        };
        /// <summary>
        /// É um método que retorna falso no SingleResponse.
        /// </summary>
        /// <param name="erros"></param>
        /// <returns></returns>
        public SingleResponse<T> CreateFailureSingleResponse(List<string> erros) => new()
        {
            HasSuccess = false,
            Message =erros.ToString()
        };
        /// <summary>
        /// É um método que retorna falso no SingleResponse.
        /// </summary>
        /// <returns></returns>
        public SingleResponse<T> CreateFailureSingleResponse()
        {
            return new SingleResponse<T>()
            {
                HasSuccess = false,
                Message = "Operação falhou",
            };
        }
        /// <summary>
        /// É um método que retorna falso no SingleResponse.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public SingleResponse<T> CreateFailureSingleResponse(T item, string message)
        {
            return new SingleResponse<T>()
            {
                HasSuccess = false,
                Message = message,
                Item = item,
            };
        }
        /// <summary>
        /// É um método que retorna falso no SingleResponse.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public SingleResponse<T> CreateFailureSingleResponse(string message)
        {
            return new SingleResponse<T>()
            {
                HasSuccess = false,
                Message = message,
            };
        }
    }
}
