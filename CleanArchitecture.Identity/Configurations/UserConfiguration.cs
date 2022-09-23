using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations
{
    // Mapeamos a ApplicationUser
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        // Implementamos la interface. 
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            // Para crear un password Hasehado
            var hasher = new PasswordHasher<ApplicationUser>();
            
            builder.HasData(
                    new ApplicationUser
                    {
                        // Podemos usar un GUID generator
                        Id = "f284b3fd-f2cf-476e-a9b6-6560689cc48c",
                        Email = "admin@locahost.com",
                        NormalizedEmail = "admin@locahost.com",
                        Nombre = "Vaxi",
                        Apellidos = "Drez",
                        UserName = "vaxidrez",
                        NormalizedUserName = "vaxidrez",
                        PasswordHash = hasher.HashPassword(null, "VaxiDrez2025$"),
                        EmailConfirmed = true,
                    },
                    new ApplicationUser
                    {
                        Id = "294d249b-9b57-48c1-9689-11a91abb6447",
                        Email = "juanperez@locahost.com",
                        NormalizedEmail = "juanperez@locahost.com",
                        Nombre = "Juan",
                        Apellidos = "Perez",
                        UserName = "juanperez",
                        NormalizedUserName = "juanperez",
                        PasswordHash = hasher.HashPassword(null, "VaxiDrez2025$"),
                        EmailConfirmed = true,
                    }

                );


        }
    }
}
