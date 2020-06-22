using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Data
{
    /// <summary>
    /// Classe servant uniquement à la création / migration des tables de la base de données avec le CLI dotnet ef [...]
    /// VS indique 0 référence mais elle sert bien !
    /// </summary>
    public class DefaultDesignTimeDbContextFactory : IDesignTimeDbContextFactory<DefaultContext>
    {
        public DefaultContext CreateDbContext(string[] args)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "..", "TrainerAPI"); //TODO adapter
            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (env == null)
                env = "Development";
            Console.WriteLine("ASPNETCORE_ENVIRONMENT is : " + env);
            IConfigurationBuilder builder = new ConfigurationBuilder()
                               .SetBasePath(path)
                               .AddJsonFile($"appsettings.{env}.json");
            IConfigurationRoot config = builder.Build();
            DbContextOptionsBuilder<DefaultContext> optionBuilder = new DbContextOptionsBuilder<DefaultContext>();
            optionBuilder.UseSqlServer(config.GetConnectionString("AdminDbContext"));
            return new DefaultContext(optionBuilder.Options);
        }
    }
}