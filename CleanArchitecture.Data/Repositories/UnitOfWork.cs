using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Infrastructure.Persistence;
using System.Collections;

namespace CleanArchitecture.Infrastructure.Repositories
{

    // Clase para implementar la lógica del UnitOfWork
    public class UnitOfWork : IUnitOfWork
    {
        // Colección de referencias a los servicios repositorios
        private Hashtable _repositories;

        private readonly StreamerDbContext _context;


        // Implementamos los métodos personalizados. 
        private IVideoRepository _videoRepository;
        private IStreamerRepository _streamerRepository;

        // Vamos a inyectarlos en vez de a través del constructor, a través de propiedades
        // No nulo
        public IVideoRepository VideoRepository => _videoRepository ??= new VideoRepository(_context);

        public IStreamerRepository StreamerRepository => _streamerRepository ??= new StreamerRepository(_context);

        public UnitOfWork(StreamerDbContext context)
        {
            _context = context;
        }

        public StreamerDbContext StreamerDbContext => _context;

        public async Task<int> Complete()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex) {
                throw new Exception("Err");
            }
            
        }

        
        // Eliminar el context cuando la transacción haya finalizado
        public void Dispose()
        {
           _context.Dispose();
        }

        public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel
        {
            // Si la colección de repositorios es nula, instancias
            if (_repositories == null)
            { 
                _repositories = new Hashtable();
            }

            // Nombre de la entidad
            var type = typeof(TEntity).Name;


            // Si en el repositorio no existe el nombre de la entidad, lo agregamos
            if (!_repositories.ContainsKey(type))
            {
                // LLamamos a la implementación de esta interface.
                var repositoryType = typeof(RepositoryBase<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(type, repositoryInstance);
            }

            return (IAsyncRepository<TEntity>)_repositories[type];
        }

    
    }
}
