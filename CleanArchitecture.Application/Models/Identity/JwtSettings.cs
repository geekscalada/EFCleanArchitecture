namespace CleanArchitecture.Application.Models.Identity
{
    public class JwtSettings
    {
        public string Key { get; set; } = string.Empty;
        
        // Quien es el que está enviando el token hacia la audiencia (clientes)
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;

        // Tiempo de vida
        public double DurationInMinutes { get; set; } 

    }
}
