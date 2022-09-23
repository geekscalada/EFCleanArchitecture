using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Identity.Models
{
    // Esta clase debe de heredar desde el paquete identity
    // Nos da una entidad usuario
    // Podemos ver las propiedades con el go to definitions
    public class ApplicationUser : IdentityUser
    {
        public string Nombre { get; set; } = string.Empty;

        public string Apellidos { get; set; } = string.Empty;
    }
}
