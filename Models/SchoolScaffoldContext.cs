using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebMVC.Models;

public partial class SchoolScaffoldContext : DbContext
{
    public SchoolScaffoldContext()
    {
    }

    public SchoolScaffoldContext(DbContextOptions<SchoolScaffoldContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MstProfile> MstProfiles { get; set; }

    public virtual DbSet<MstUser> MstUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MstProfile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mst_profile_pk");

            entity.ToTable("mst_profile");

            entity.HasIndex(e => e.UserId, "mst_profile_unique").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Alamat).HasColumnName("alamat");
            entity.Property(e => e.NamaLengkap)
                .HasColumnType("character varying")
                .HasColumnName("nama_lengkap");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithOne(p => p.MstProfile)
                .HasForeignKey<MstProfile>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("mst_profile_mst_user_fk");
        });

        modelBuilder.Entity<MstUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mst_user_pk");

            entity.ToTable("mst_user");

            entity.HasIndex(e => e.UserName, "mst_user_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Pekerjaan)
                .HasColumnType("character varying")
                .HasColumnName("pekerjaan");
            entity.Property(e => e.UserName)
                .HasColumnType("character varying")
                .HasColumnName("user_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
