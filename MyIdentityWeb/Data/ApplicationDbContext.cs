using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyIdentityWeb.Models;

namespace MyIdentityWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        // Add DbSet properties for your entities here
        // public DbSet<YourEntity> YourEntities { get; set; }

        public virtual DbSet<Benefit> Benefits { get; set; }

        public virtual DbSet<BenefitEmployee> BenefitEmployees { get; set; }

        public virtual DbSet<Department> Departments { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Benefit>(entity =>
            {
                entity.Property(e => e.BenefitName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<BenefitEmployee>(entity =>
            {
                entity.ToTable("BenefitEmployee");

                entity.HasOne(d => d.Benefit).WithMany(p => p.BenefitEmployees)
                    .HasForeignKey(d => d.BenefitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BenefitEmployee_Benefits");

                entity.HasOne(d => d.Employee).WithMany(p => p.BenefitEmployees)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_BenefitEmployee_Employees");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasIndex(e => e.EmployeeId, "Unique_Employees").IsUnique();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(e => e.EmployeeIdMasking)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employees_Departments");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
