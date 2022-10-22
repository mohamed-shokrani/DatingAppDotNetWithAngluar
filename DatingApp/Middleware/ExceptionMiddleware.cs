using DatingApp.Errors;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Text.Json;

namespace DatingApp.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next //RequestDelegate is what is comming next in middlware  pipline
            , ILogger<ExceptionMiddleware> logger // so we can still logout our exception into the terminal we do not wanna swoalow 
        , IHostEnvironment env // to check what env we are running in are we in production are we in dev 
             )
        {
            _next = next;
            _logger = logger;
            _env = env;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // this piece of middleware is gonna live on a very top of our middleware
                // and anything below this gets an exception they are gonna throw the exception up and up 
                //until they reach something that handle the exception 

            }
            catch (Exception ex)
            {
                _logger.LogError( ex, ex.Message);

                context.Response.ContentType = "application/json";

                context.Response.StatusCode =(int) HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment()

                    ? new ApiException(context.Response.StatusCode,ex.Message,ex?.StackTrace.ToString())

                    : new ApiException(context.Response.StatusCode,"Internal Server Error");

                var Options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }; // we want  our JSON reponses to get our code back in cammal case 
                    //because we need to serialize this response into a JSON response 
                    var json = JsonSerializer.Serialize(response, Options);
                await context.Response.WriteAsync(json);

            }
        }


    }
}
