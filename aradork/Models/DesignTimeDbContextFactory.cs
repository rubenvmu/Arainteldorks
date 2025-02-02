using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using aradork.Models;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<aradorkDBContext>
{
    public aradorkDBContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<aradorkDBContext>();

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("AradorkConnection");

        optionsBuilder.UseSqlite(connectionString);  // Usar UseSqlite en lugar de UseSqlServer

        return new aradorkDBContext(optionsBuilder.Options);
    }
}