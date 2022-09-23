using CleanArchitecture.API.Errors;
using CleanArchitecture.Application.Exceptions;
using Newtonsoft.Json;
using System.Net;

// OJo este código no es el mostrado en los videos

namespace CleanArchitecture.API.Middleware
{
    public class ExceptionMiddleware
    {
        // este representa el backline que va a continuar a la siguiente linea
        // en caso de que no ocurra ninguna excepción
        private readonly RequestDelegate _next;

        // imprimir mensaje de error
        private readonly ILogger<ExceptionMiddleware> _logger;

        // Lo necesitamos para Saber si estamos en dev o en prod
        private readonly IHostEnvironment _env;

        //Constructor
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // El context es el delegate
                await _next(context);
            }
            catch (Exception ex) {
                _logger.LogError(ex, ex.Message);

                // Queremos enviar un request al cliente. 
                context.Response.ContentType = "application/json";
                //Status por defecto es un error 500
                var statusCode = (int)HttpStatusCode.InternalServerError;
                // Mensaje detalle de la excepción
                var result = string.Empty;


                switch (ex)
                {
                    // cuando no encontremos el objeto que estamos buscando
                    case NotFoundException notFoundException:
                        statusCode = (int)HttpStatusCode.NotFound;
                        break;
                    
                    // cuando ocurra una excepción por validación
                    case ValidationException validationException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        // Nos devuelve la lista de errores en validación, convertidas a JSON
                        var validationJson = JsonConvert.SerializeObject(validationException.Errors);

                        // LLamamos a CodeErrorException
                        result = JsonConvert.SerializeObject(new CodeErrorException(statusCode, ex.Message, validationJson));
                        break;
                    

                    case BadRequestException badRequestException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    default:
                        break;
                }

                // En caso de que el result esté en blanco, queremos volver a iniciar el CodeErrorException
                // StackTrace es el detalle de la excepción
                if (string.IsNullOrEmpty(result))
                    result = JsonConvert.SerializeObject(new CodeErrorException(statusCode, ex.Message, ex.StackTrace));


                context.Response.StatusCode = statusCode;

                await context.Response.WriteAsync(result);

            }
        
        }

    }
}
