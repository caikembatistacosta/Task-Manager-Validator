using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ResponseFactory
    {
        private static ResponseFactory _factory;

        public static ResponseFactory CreateInstance()
        {
            _factory ??= new ResponseFactory();
            return _factory;
        }
        public Response CreateSuccessResponse()
        {
            return new Response()
            {
                HasSuccess = true,
                Message = "Operação realizada com sucesso"
            };
        }
        public Response CreateFailureResponse(Exception ex)
        {
            return new Response()
            {
                HasSuccess = false,
                Message = "Operação falhou",
                Exception = ex
            };
        }
        public Response CreateFailureResponse()
        {
            return new Response()
            {
                HasSuccess = false,
                Message = "Operação falhou",
            };
        }

    }
}
