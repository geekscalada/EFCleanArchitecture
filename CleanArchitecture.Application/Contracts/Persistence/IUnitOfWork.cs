using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        
        // Declaramos métodos que instancian al repositorio de Streamer y de video. 
        IStreamerRepository StreamerRepository { get; }
        IVideoRepository VideoRepository { get; }
        
        // metodo genérico que nos devuelve la instancia del servicio o repositorio que vamos a usar
        //TEntity = genérico
        IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel;

        // Método para saber cuando una transacción ya ha terminado
        Task<int> Complete();
    }
}
