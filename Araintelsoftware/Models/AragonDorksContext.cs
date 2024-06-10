// Araintelsoftware.Models

// Araintelsoftware.Data
using Araintelsoftware.Models;
using Microsoft.EntityFrameworkCore;

namespace Araintelsoftware.Data
{

    public class AragonDorksContext : DbContext 
    {
        private readonly IConfiguration _configuration;

        public AragonDorksContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<AragonDork> AragonDorks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var sqlServerConfig = _configuration.GetSection("SqlServer");

            var connectionString = $"Server={sqlServerConfig["Server"]};Database={sqlServerConfig["Database"]};User ID={sqlServerConfig["User"]};Password={sqlServerConfig["Password"]};Trusted_Connection=False;MultipleActiveResultSets=true";

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}