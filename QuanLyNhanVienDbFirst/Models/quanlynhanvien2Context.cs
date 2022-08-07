using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QuanLyNhanVienDbFirst.Models
{
    public partial class quanlynhanvien2Context : DbContext
    {
        public quanlynhanvien2Context()
        {
        }

        public quanlynhanvien2Context(DbContextOptions<quanlynhanvien2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=127.0.0.1;user=root;database=quanlynhanvien2", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.18-mariadb"));
            }*/
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.IdDepartment)
                    .HasName("PRIMARY");

                entity.ToTable("departments");

                entity.Property(e => e.IdDepartment).HasColumnType("int(11)");

                entity.Property(e => e.NameDepartment).HasMaxLength(50);

                entity.Property(e => e.Quantity).HasColumnType("int(11)");
            });

            modelBuilder.Entity<Efmigrationshistory>(entity =>
            {
                entity.HasKey(e => e.MigrationId)
                    .HasName("PRIMARY");

                entity.ToTable("__efmigrationshistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ProductVersion).HasMaxLength(32);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employees");

                entity.HasIndex(e => e.DepartmentId, "IX_Employees_DepartmentId");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.DepartmentId).HasColumnType("int(11)");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Salary).HasColumnType("int(11)");

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_Employees_Departments_DepartmentId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
