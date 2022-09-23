using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Contracts.Persistence
{

    // Te obliga a hacer <video> porque al ser genérica, te olbiga a decirle a qué clase
    // vas a hacer mantenimiento, no vale T. 

    //IVideoRepository hereda de IAsyncRepository
    public interface IVideoRepository : IAsyncRepository<Video>
    {

        //busqueda de videos por el nombre del video
        Task<Video> GetVideoByNombre(string nombreVideo);
        
        
        Task<IEnumerable<Video>> GetVideoByUsername(string username);
    }
}
