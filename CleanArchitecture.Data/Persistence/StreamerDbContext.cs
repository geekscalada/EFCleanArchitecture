using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Persistence
{
    public class StreamerDbContext : DbContext
    {

        // Constructor
        // DbContextOptions incluye la característica de la cadena de conexion
        public StreamerDbContext(DbContextOptions<StreamerDbContext> options) : base(options)
        {
        }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Data Source=localhost; 
        //        Initial Catalog=Streamer;user id=sa;password=VaxiDrez2025$")
        //    .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, Microsoft.Extensions.Logging.LogLevel.Information)
        //    .EnableSensitiveDataLogging();
        //}


        // este método se va a ejecutar siempre antes de ahcer un create o un update
        // se ejecutará por defecto y después por ejemplo con el seed, se setearán unos valores posteriormente,
        // se sobreescribirán
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseDomainModel>())
            {
                switch (entry.State)
                { 
                    case EntityState.Added:
                        entry.Entity.CreatedDate =  DateTime.Now;
                        entry.Entity.CreatedBy = "system";
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = "system";
                        break;
                }
            }
            
            // aquí hacemos el commit
            return base.SaveChangesAsync(cancellationToken);
        }


        //Nomenclatura de Fluent API
        // Si sigues las convenciones de EF usando el tema de las anclas y las nomenclaturas
        // y usas correctamente las clases, no necesitas esta nomeclatura adicional
        // aunque muchos programadores lo usan como una buean práctica para asegurarse
        // de la mantenibilidad del código
        // estarás obligado a usarlo cuadno veas que en un código otro programador 
        // no ha usado la nomenclatura de las FK y por lo tanto EF no reconoce las 
        // FK, de manera que tenemos que forzarlas nosotros

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Streamer>()
                .HasMany(m => m.Videos)
                .WithOne(m => m.Streamer)
                .HasForeignKey(m => m.StreamerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<Video>()
                .HasMany(p => p.Actores)
                .WithMany(t => t.Videos)
                .UsingEntity<VideoActor>(
                    pt => pt.HasKey(e => new { e.ActorId, e.VideoId })
                );

        
        }


        // Nullable

        public DbSet<Streamer>? Streamers { get; set; }

        public DbSet<Video>? Videos { get; set; }

        public DbSet<Actor>? Actores { get; set; }

        public DbSet<Director>? Directores { get; set; }

    }
}
