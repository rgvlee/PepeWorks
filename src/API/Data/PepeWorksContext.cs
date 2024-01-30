using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public partial class PepeWorksContext : DbContext
{
    public PepeWorksContext()
    {
    }

    public PepeWorksContext(DbContextOptions<PepeWorksContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Camp> Camps { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<SchemaVersion> SchemaVersions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MOE\\SQL2022; Database=PepeWorks; Trusted_connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Camp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Camp__3214EC0741E14F16");

            entity
                .ToTable("Camp")
                .ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("MSSQL_TemporalHistoryFor_981578535", "dbo");
                        ttb
                            .HasPeriodStart("SysStartTime")
                            .HasColumnName("SysStartTime");
                        ttb
                            .HasPeriodEnd("SysEndTime")
                            .HasColumnName("SysEndTime");
                    }));

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);

            entity.HasOne(d => d.Location).WithMany(p => p.Camps)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Camp__LocationId__3C69FB99");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Location__3214EC070A533ECC");

            entity
                .ToTable("Location")
                .ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("MSSQL_TemporalHistoryFor_933578364", "dbo");
                        ttb
                            .HasPeriodStart("SysStartTime")
                            .HasColumnName("SysStartTime");
                        ttb
                            .HasPeriodEnd("SysEndTime")
                            .HasColumnName("SysEndTime");
                    }));

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Room__3214EC07038F8E09");

            entity
                .ToTable("Room")
                .ToTable(tb => tb.IsTemporal(ttb =>
                    {
                        ttb.UseHistoryTable("MSSQL_TemporalHistoryFor_1045578763", "dbo");
                        ttb
                            .HasPeriodStart("SysStartTime")
                            .HasColumnName("SysStartTime");
                        ttb
                            .HasPeriodEnd("SysEndTime")
                            .HasColumnName("SysEndTime");
                    }));

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);

            entity.HasOne(d => d.Camp).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.CampId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Room__CampId__403A8C7D");
        });

        modelBuilder.Entity<SchemaVersion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SchemaVersions_Id");

            entity.Property(e => e.Applied).HasColumnType("datetime");
            entity.Property(e => e.ScriptName).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
