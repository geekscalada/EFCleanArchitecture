namespace CleanArchitecture.Application.Models.Identity
{
    public class RegistrationResponse
    {

        // Clase que utilizaremos como responde en caso de que el registro
        // sea exitoso

        // Basicamente parece una buena prácta que sea similar o = al authresponse
        public string UserId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
