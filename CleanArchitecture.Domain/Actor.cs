
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain
{
    public class Actor : BaseDomainModel
    {
        public Actor() {
            // De esta manera estaríamos inicializando la colección de videos dentro de actor
            // Aquí parece que estamos convirtiendo el ICollection en un HashSet
            Videos = new HashSet<Video>();
        }
        
        public string? Nombre { get; set; }

        public string? Apellido { get; set; }

        public virtual ICollection<Video> Videos { get; set; }

    }
}
