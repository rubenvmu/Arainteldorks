/*using Microsoft.EntityFrameworkCore;
using Araintelsoftware.Models;
using Araintelsoftware.Areas.Identity.Data;
using Araintelsoftware.Data;

namespace Araintelsoftware.Data
{
    public class AraintelsoftDBContext : DbContext
    {
        public AraintelsoftDBContext(DbContextOptions<AraintelsoftDBContext> options) : base(options)
        {
        }

        public DbSet<SampleUser> Users { get; set; }
        public DbSet<OtherEntity> OtherEntities { get; set; }
        // Agrega aquí las demás entidades que necesites

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SampleUser>().ToTable("AspNetUsers");
            modelBuilder.Entity<OtherEntity>().ToTable("OtherEntities");
            // Agrega aquí las demás configuraciones de entidades que necesites
        }
    }

    public class AraintelsqlContext : DbContext
    {
        public AraintelsqlContext(DbContextOptions<AraintelsqlContext> options) : base(options)
        {
        }

        // Agrega aquí las entidades y configuraciones específicas para esta base de datos
    }
}

var builder = WebApplication.CreateBuilder(args);

//...

builder.Services.AddDbContext<AraintelsoftDBContext>(options =>
{
    options.UseSqlServer(araintelsoftConnectionString);
});

builder.Services.AddDbContext<AraintelsqlContext>(options =>
{
    options.UseSqlServer(araintelsqlConnectionString);
});

//...

*/