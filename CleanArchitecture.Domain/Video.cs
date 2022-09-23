using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain
{
    public class Video : BaseDomainModel
    {
        // Constructor
        public Video() 
        {
            Actores = new HashSet<Actor>();
        }
        
        public string? Nombre { get; set; }

        //Ancla para clave
        // Cuando le damos la propiedad virtual significa que puede ser
        // sobreescrita por una clase derivada en un futuro    

        public int StreamerId { get; set; }

       public virtual Streamer? Streamer { get; set; }

        public virtual ICollection<Actor> Actores { get; set; }

        public virtual Director Director { get; set; }

    }
}
