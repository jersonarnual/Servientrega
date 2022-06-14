using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Servientrega.Data.Context;
using Svientrega.Api;
using System;

namespace Servientrega.Api.Extension
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            using (var appContext = scope.ServiceProvider.GetRequiredService<ServientregaContext>())
            {
                try
                {
                    appContext.Database.Migrate();
                    appContext.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred configuring the DB.");
                }
            }

            return host;
        }
    }
}
