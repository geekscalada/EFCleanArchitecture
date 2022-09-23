namespace CleanArchitecture.API.Errors
{
    public class CodeErrorResponse
    {
       public int StatusCode { get; set; }

       public string? Message { get; set; }

        // Estructura del mensaje que le voy a enviar al cliente

        // message es nullable y por defecto es nulo ya que elservidor podría devolvernos el codigo pero no 
        // nos devuelva mensaje
        public CodeErrorResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;

            // si el mensaje es nulo, entonces queremos que el que se encargue de procesar el error sea el getDefaultMessageStatusCode
            Message = message ?? GetDefaultMessageStatusCode(statusCode);
        }

        private string GetDefaultMessageStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "El Request enviado tiene errores",
                401 => "No tienes authorizacion para este recurso",
                404 => "No se encontro el recurso solicitado",
                500 => "Se produjeron errores en el servidor",
                // Valor por defecto
                _ => string.Empty
            };
        }
    }
}
