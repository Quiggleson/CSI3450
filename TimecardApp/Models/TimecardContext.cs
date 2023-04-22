using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TimecardApp.Models;

public partial class TimecardContext : DbContext
{
    public TimecardContext()
    {
    }

    public TimecardContext(DbContextOptions<TimecardContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<TimeRecord> TimeRecords { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("name=ConnectionStrings:Default", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EId).HasName("PRIMARY");

            entity.HasIndex(e => e.ManagerId, "fk_Employees_Employees_idx");

            entity.Property(e => e.EId)
                .ValueGeneratedNever()
                .HasColumnName("eId");
            entity.Property(e => e.EFirstName)
                .HasMaxLength(45)
                .HasColumnName("eFirstName");
            entity.Property(e => e.ELastName)
                .HasMaxLength(45)
                .HasColumnName("eLastName");
            entity.Property(e => e.EPassword)
                .HasMaxLength(256)
                .HasColumnName("ePassword");
            entity.Property(e => e.ETitle)
                .HasMaxLength(45)
                .HasColumnName("eTitle");
            entity.Property(e => e.ManagerId).HasColumnName("managerId");

            entity.HasOne(d => d.Manager).WithMany(p => p.InverseManager)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("fk_Employees_Employees");
        });

        modelBuilder.Entity<TimeRecord>(entity =>
        {
            entity.HasKey(e => new { e.TimeIn, e.EId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("TimeRecord");

            entity.HasIndex(e => e.EId, "fk_TimeRecord_Employees1_idx");

            entity.Property(e => e.TimeIn)
                .HasColumnType("datetime")
                .HasColumnName("timeIn");
            entity.Property(e => e.EId).HasColumnName("eId");
            entity.Property(e => e.IsStarred).HasColumnName("isStarred");
            entity.Property(e => e.TimeOut)
                .HasColumnType("datetime")
                .HasColumnName("timeOut");

            entity.HasOne(d => d.EIdNavigation).WithMany(p => p.TimeRecords)
                .HasForeignKey(d => d.EId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_TimeRecord_Employees1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
