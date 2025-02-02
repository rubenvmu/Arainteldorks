using Microsoft.EntityFrameworkCore;
using aradork.Models;

namespace aradork.Models
{
    public partial class aradorkDBContext : DbContext
    {
        public aradorkDBContext(DbContextOptions<aradorkDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Agendum> Agenda { get; set; }
        public virtual DbSet<Secrets> Secrets { get; set; }
        public virtual DbSet<AragonDorks> AragonDorks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agendum>(entity =>
            {
                entity.HasKey(e => e.Firstname).HasName("PK__agenda__071F893BEFEC13FF");
                entity.ToTable("agenda");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(364)
                    .IsUnicode(false);
                entity.Property(e => e.Company)
                    .HasMaxLength(68)
                    .IsUnicode(false);
                entity.Property(e => e.LastConnection)
                    .HasMaxLength(12)
                    .IsUnicode(false);
                entity.Property(e => e.Lastname)
                    .HasMaxLength(48)
                    .IsUnicode(false);
                entity.Property(e => e.Mail)
                    .HasMaxLength(39)
                    .IsUnicode(false);
                entity.Property(e => e.Postion)
                    .HasMaxLength(90)
                    .IsUnicode(false);
                entity.Property(e => e.Url)
                    .HasMaxLength(74)
                    .IsUnicode(false)
                    .HasColumnName("URL");
            });

            modelBuilder.Entity<AragonDorks>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("AragonDorks");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}