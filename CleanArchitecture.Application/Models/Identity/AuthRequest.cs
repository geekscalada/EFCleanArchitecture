namespace CleanArchitecture.Application.Models.Identity
{
    // Aquí empezaremos a usar JWT para sesiones, identidad, seguridad...
    public class AuthRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
