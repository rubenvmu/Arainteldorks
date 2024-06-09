using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Araintelsoftware.Models;

namespace Araintelsoftware.Models;

public partial class AraintelsqlContext : DbContext
{
    public AraintelsqlContext()
    {
    }

    public AraintelsqlContext(DbContextOptions<AraintelsqlContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Agendum> Agenda { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=araintelsql.database.windows.net;Database=araintelsql;User=AraintelAdministrator;Password=AraintelSQL*");

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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

public DbSet<Araintelsoftware.Models.Secrets> Secrets { get; set; } = default!;
}
