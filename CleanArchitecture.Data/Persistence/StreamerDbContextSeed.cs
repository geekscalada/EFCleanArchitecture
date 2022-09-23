﻿using CleanArchitecture.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Persistence
{
    public class StreamerDbContextSeed

    // Añadimos data
    {
        public static async Task SeedAsync(StreamerDbContext context, ILogger<StreamerDbContextSeed> logger)
        {
            // Si no tiene datos
            if (!context.Streamers!.Any())
            {
                // rellenamos. ?  ahora crearemos un método que nos permita llenar con data hardcoreada
                context.Streamers!.AddRange(GetPreconfiguredStreamer());

                // Ahora ya podemos llamar al SaveChangesAsync.
                await context.SaveChangesAsync();
                logger.LogInformation("Estamos insertando nuevos records al db {context}", typeof(StreamerDbContext).Name);
            }
        
        }

        private static IEnumerable<Streamer> GetPreconfiguredStreamer()
        {
            return new List<Streamer>
            {
                new Streamer {CreatedBy = "vaxidrez", Nombre = "Maxi HBP", Url = "http://www.hbp.com" },
                new Streamer {CreatedBy = "vaxidrez", Nombre = "Amazon VIP", Url = "http://www.amazonvip.com" },
            };
        
        }


    }
}
