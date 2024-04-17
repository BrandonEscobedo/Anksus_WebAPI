using Anksus_WebAPI.Models.dbModels;
using Microsoft.EntityFrameworkCore;

namespace Anksus_WebAPI.Server.ExtensionMigrations
{
    public static class MigrationExtension
    {
        public static void ApplyMigration(this WebApplication application)
        {
            using var scope=application.Services.CreateScope();
            var dbcontext = scope.ServiceProvider.GetRequiredService<TestAnskusContext>();
            dbcontext.Database.Migrate();
        }

    }
}
