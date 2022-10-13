using Entities;
using log4net;
using System.Net;

namespace WebApi.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized || httpContext.Response.StatusCode == (int)HttpStatusCode.Forbidden)
            {
                throw new UnauthorizedAccessException();
            }
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _log.Error($"Algo deu errado {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        
        }
        public async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var errorsDetails = new ErrorsDetails()
            {
                StatusCode = 0,
                Message = null
            };
            switch (exception)
            {
                case NullReferenceException ex:
                    errorsDetails.StatusCode = (int)HttpStatusCode.NotFound;
                    errorsDetails.Message = ex.Message;
                    break;
                case ArgumentNullException ex:
                    errorsDetails.StatusCode = (int)HttpStatusCode.NotFound;
                    errorsDetails.Message = ex.Message;
                    break;
                case UnauthorizedAccessException ex:
                    errorsDetails.StatusCode = (int)HttpStatusCode.Unauthorized;
                    errorsDetails.Message = ex.Message;
                    break;
                default:
                    httpContext.Response.ContentType = "application/json";
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            _log.Error("Uma exceção nula foi gerada", exception);
            await httpContext.Response.WriteAsync(errorsDetails.ToJsonSerealize());
        }

    }
}
