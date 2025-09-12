using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace domain.Data
{
    public class AssuranceDbContextFactory : IDesignTimeDbContextFactory<AssuranceDbContext>
    {
        public AssuranceDbContext CreateDbContext(string[] args)
        {
            // Configuration pour les migrations
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AssuranceDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString,
                sqlOptions => sqlOptions.MigrationsAssembly("data"));

            return new AssuranceDbContext(optionsBuilder.Options);
        }
    }

}