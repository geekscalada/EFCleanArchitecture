using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain
{
    public class Streamer : BaseDomainModel
    {
        // por convención algo que termine en Id, EF lo considerará PK
        // En este caso recuerda que Id lo estás heredando de BaseDomainModel
        public string? Nombre { get; set; }
        // si le ponemos = string.Empty; Le damos valor de string vació para que nos 
        //desaparezca el warning de que no es nullable

        //La otra opción es hacer que sea nullable con el ?
        public string? Url { get; set; }

        public ICollection<Video>? Videos { get; set; }

    }
}
