using Araintelsoftware.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Araintelsoftware.Data
{
    public class AraintelsoftDBContext : IdentityDbContext<SampleUser, IdentityRole<string>, string>
    {
        public AraintelsoftDBContext(DbContextOptions<AraintelsoftDBContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
        }

        public override DbSet<SampleUser> Users { get; set; }

        public override DbSet<IdentityRole<string>> Roles { get; set; }

        public override DbSet<IdentityUserRole<string>> UserRoles { get; set; }

        public override DbSet<IdentityUserClaim<string>> UserClaims { get; set; }

        public override DbSet<IdentityUserLogin<string>> UserLogins { get; set; }

        public override DbSet<IdentityUserToken<string>> UserTokens { get; set; }
    }

    public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<SampleUser>

    {

        public void Configure(EntityTypeBuilder<SampleUser> builder)

        {

            builder.Property(u => u.FirstName).HasMaxLength(30);

            builder.Property(u => u.LastName).HasMaxLength(40);

            builder.Property(u => u.Birthdate).HasColumnType("date");

        }

    }
}