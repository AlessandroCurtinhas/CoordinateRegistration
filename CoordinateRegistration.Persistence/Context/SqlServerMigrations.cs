using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CoordinateRegistration.Persistence.Context
{

    namespace ApiEmpresas.Infra.Data.Contexts
    {
        public class SqlServerMigrations : IDesignTimeDbContextFactory<CoordinateRegistrationDbContext>
        {

            public CoordinateRegistrationDbContext CreateDbContext(string[] args)
            {

                var configurationBuilder = new ConfigurationBuilder();
                var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
                configurationBuilder.AddJsonFile(path, false);

                var root = configurationBuilder.Build();
                var connectionString = root.GetSection("ConnectionStrings")
                    .GetSection("BikeRoute").Value;

                var builder = new DbContextOptionsBuilder<CoordinateRegistrationDbContext>();

                builder.UseSqlServer(connectionString);

                return new CoordinateRegistrationDbContext(builder.Options);
            }

        }
    }
}
