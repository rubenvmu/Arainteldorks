/*using Microsoft.EntityFrameworkCore;
using Araintelsoftware.Models;
using Araintelsoftware.Areas.Identity.Data;

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
}

*/