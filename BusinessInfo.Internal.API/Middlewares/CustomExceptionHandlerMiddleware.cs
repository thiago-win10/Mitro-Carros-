using BusinessInfo.Application.Common.Exceptions;
using BusinessInfo.Application.Common.Models.Response;
using BusinessInfo.Common;
using System.Net;

namespace BusinessInfo.Internal.API.Middlewares
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly ILogger<CustomExceptionHandlerMiddleware> _log;
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> log)
        {
            _next = next;
            _log = log;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _log.LogError(exception, exception.Message);

            var code = HttpStatusCode.InternalServerError;

            string result = null;
            switch (exception)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;

                    result = new
                    {
                        Errors = validationException.Failures?.Select(x => new ResponseApiError
                        {
                            Key = x.Key,
                            Value = x.Value
                        })

                    }.ToJson();
                    break;
                case BadRequestException badRequestException:
                    code = HttpStatusCode.BadRequest;
                    result = new
                    {
                        Errors = new ResponseApiError[1]
                        {
                            new ResponseApiError
                            {
                                Value = badRequestException.Message
                            }
                        }
                    }.ToJson();
                    break;
                case NotFoundException _:
                    code = HttpStatusCode.NotFound;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (result == null)
                result = new
                {
                    Errors = new ResponseApiError[1]
                    {
                        new ResponseApiError
                        {
                            Value = exception.Message
                        }
                    }
                }.ToJson();
            return context.Response.WriteAsync(result);
        }
    }
}
