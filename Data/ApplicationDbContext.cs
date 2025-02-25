using HRMSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace HRMSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<EmploymentContract> Contracts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Gọi Identity configuration trước

            // Cấu hình quan hệ 1-1 giữa Employee và User (ASP.NET Identity)
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.User)
                .WithOne()
                .HasForeignKey<Employee>(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Tránh lỗi khi xóa User

            // Cấu hình quan hệ 1-1 giữa Employee và EmploymentContract
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Contract)
                .WithOne(c => c.Employee)
                .HasForeignKey<EmploymentContract>(c => c.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Cấu hình quan hệ 1-1 giữa Employee và Salary
            modelBuilder.Entity<Salary>()
                .HasOne(s => s.Employee)
                .WithOne(e => e.Salary)
                .HasForeignKey<Salary>(s => s.EmployeeId) // Đặt EmployeeId là khóa ngoại trong Salary
                .OnDelete(DeleteBehavior.Cascade); // Xóa Employee sẽ xóa luôn Salary

            modelBuilder.Entity<EmploymentContract>()
                .Property(c => c.Salary)
                .HasColumnType("decimal(18,4)");

            modelBuilder.Entity<Salary>()
                .Property(s => s.Amount)
                .HasColumnType("decimal(18,4)");
        }
    }
}
