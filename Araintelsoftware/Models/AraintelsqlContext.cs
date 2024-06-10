using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Araintelsoftware.Models;

namespace Araintelsoftware.Models;

public partial class AraintelsqlContext : DbContext
{
    private readonly IConfiguration _configuration;


    public AraintelsqlContext()

    {

    }

    public AraintelsqlContext(DbContextOptions<AraintelsqlContext> options, IConfiguration configuration)

        : base(options)

    {

        _configuration = configuration;

    }

    public virtual DbSet<Agendum> Agenda { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

    {

        var sqlServerConfig = _configuration.GetSection("SqlServer");

        var connectionString = $"Server={sqlServerConfig["Server"]};Database={sqlServerConfig["Database"]};User ID={sqlServerConfig["User"]};Password={sqlServerConfig["Password"]};Trusted_Connection=False;MultipleActiveResultSets=true";

        optionsBuilder.UseSqlServer(connectionString);

    }

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
