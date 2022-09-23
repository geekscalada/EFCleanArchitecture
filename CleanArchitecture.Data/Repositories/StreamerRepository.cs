using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistence;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class StreamerRepository : RepositoryBase<Streamer>, IStreamerRepository
    {
        // En el IStreamerrepository vemos que no hay 
        // entonces lo que hacemos es declarar el constructor que inicialice la cadena de conexión e instancie
        // el dbcontext que conecta con la BDD
        public StreamerRepository(StreamerDbContext context) : base(context)
        { }
    }
}
