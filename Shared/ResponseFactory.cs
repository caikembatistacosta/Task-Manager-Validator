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

        public static ResponseFactory CreateInstance()
        {
            _factory ??= new ResponseFactory();
            return _factory;
        }
        public Response CreateSuccessResponse() => new()
        {
            HasSuccess = true,
            Message = "Operação realizada com sucesso"
        };
        public Response CreateFailureResponse(Exception ex) => new()
        {
            HasSuccess = false,
            Message = "Operação falhou",
            Exception = ex
        };
        public Response CreateFailureResponse() => new()
        {
            HasSuccess = false,
            Message = "Operação falhou"
        };
        public Response CreateFailureResponse(string message) => new()
        {
            HasSuccess = false,
            Message = message
        };
        public Response CreateSuccessResponse(string message) => new()
        {
            HasSuccess = true,
            Message = message
        };
    }
}
