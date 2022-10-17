using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ResponseFactory
    {
        private static ResponseFactory _factory;

        /// <summary>
        /// Cria uma instância static do ResponseFactory para seus métodos.
        /// </summary>
        /// <returns></returns>
        public static ResponseFactory CreateInstance()
        {
            _factory ??= new ResponseFactory();
            return _factory;
        }
        /// <summary>
        /// É um método que retorna sucesso no Response.
        /// </summary>
        /// <returns></returns>
        public Response CreateSuccessResponse() => new()
        {
            HasSuccess = true,
            Message = "Operação realizada com sucesso"
        };
        /// <summary>
        /// É um método que retorna falha no Response.
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public Response CreateFailureResponse(Exception ex) => new()
        {
            HasSuccess = false,
            Message = "Operação falhou",
            Exception = ex
        };
        /// <summary>
        /// É um método que retorna falha no Response.
        /// </summary>
        /// <returns></returns>
        public Response CreateFailureResponse() => new()
        {
            HasSuccess = false,
            Message = "Operação falhou"
        };
        /// <summary>
        /// É um método que retorna falha no Response.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Response CreateFailureResponse(string message) => new()
        {
            HasSuccess = false,
            Message = message
        };
        /// <summary>
        /// É um método que retorna sucesso no Reponse.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Response CreateSuccessResponse(string message) => new()
        {
            HasSuccess = true,
            Message = message
        };
    }
}
