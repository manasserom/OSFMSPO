using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication4.Models;

public partial class OsfmspoContext : DbContext
{
    public OsfmspoContext()
    {
    }

    public OsfmspoContext(DbContextOptions<OsfmspoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CustomerMaster> CustomerMasters { get; set; }

    public virtual DbSet<MaterialMaster> MaterialMasters { get; set; }

    public virtual DbSet<MaterialType> MaterialTypes { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Row> Rows { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=OSFMSPO;Trusted_Connection=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerMaster>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.ToTable("CustomerMaster");

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MaterialMaster>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.ToTable("MaterialMaster");

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MaterialType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Price).HasColumnType("money");

            entity.HasOne(d => d.MaterialTypeNavigation).WithMany(p => p.MaterialMasters)
                .HasForeignKey(d => d.MaterialType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MaterialMaster_MaterialType");
        });

        modelBuilder.Entity<MaterialType>(entity =>
        {
            entity.HasKey(e => e.MaterialTypeName);

            entity.ToTable("MaterialType");

            entity.Property(e => e.MaterialTypeName)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.Property(e => e.Customer)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TotalPrice).HasColumnType("money");

            entity.HasOne(d => d.CustomerNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Customer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_CustomerMaster");

            entity.HasOne(d => d.Row).WithMany(p => p.Orders)
                .HasForeignKey(d => d.RowId)
                .HasConstraintName("FK_Order_Row");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Status");
        });

        modelBuilder.Entity<Row>(entity =>
        {
            entity.ToTable("Row");

            entity.Property(e => e.Material)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("money");

            entity.HasOne(d => d.MaterialNavigation).WithMany(p => p.Rows)
                .HasForeignKey(d => d.Material)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Row_MaterialMaster");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.NameStatus);

            entity.ToTable("Status");

            entity.Property(e => e.NameStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
