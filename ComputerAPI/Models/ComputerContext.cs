using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ComputerAPI.Models;

public partial class ComputerContext : DbContext
{
    public ComputerContext()
    {
    }

    public ComputerContext(DbContextOptions<ComputerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comp> Comps { get; set; }

    public virtual DbSet<OSystem> Os { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("comp");

            entity.HasIndex(e => e.OsId, "OsId");

            entity.Property(e => e.Brand).HasMaxLength(37);
            entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            entity.Property(e => e.Memory).HasColumnType("int(11)");
            entity.Property(e => e.Type).HasMaxLength(30);

            entity.HasOne(d => d.Os).WithMany(p => p.Comps)
                .HasForeignKey(d => d.OsId)
                .HasConstraintName("comp_ibfk_1");
        });

        modelBuilder.Entity<OSystem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("os");

            entity.Property(e => e.Name).HasMaxLength(27);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
