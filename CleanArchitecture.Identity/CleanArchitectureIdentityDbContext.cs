using CleanArchitecture.Identity.Configurations;
using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

// Necesitamos un DBContext para Identity ya que es el que generará el almacenamiento y estructura

namespace CleanArchitecture.Identity
{

    // Heredamos de IdentityDbContext porque queremos tener las entidades de roles, usuarios, etc.
    // Estas entidades en el proceso de migration se convertirán en tablas. 

    // ApplicationUsar es una clase custom que tendrá sus propias campos adicionales, en vez de usar la entidad user por 
    // defecto que trae el identityEFCore
    public class CleanArchitectureIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        // Constructor
        // Queremos que la cadena de conexión a la BDD se inyecte directamente en el constructor (lo inyectaremos desde API)
        public CleanArchitectureIdentityDbContext(DbContextOptions<CleanArchitectureIdentityDbContext> options) : base(options)
        {
        }

        // COnfigurar entidades rol y usuario que nos trae el entityDBContext
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            
            // Usaremos esto para agregar alguna data de prueba, usuario administrador y regular dentro de la APP

            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
        }

    }
}
