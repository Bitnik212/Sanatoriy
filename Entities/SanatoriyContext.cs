using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Sanatoriy.Entities
{
    public partial class SanatoriyContext : DbContext
    {
        public SanatoriyContext()
            : base("name=SanatoriyContext")
        {
        }

        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Patients> Patients { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Services> Services { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employees>()
                .Property(e => e.FIO)
                .IsUnicode(false);

            modelBuilder.Entity<Employees>()
                .Property(e => e.Passport)
                .IsUnicode(false);

            modelBuilder.Entity<Employees>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Employees>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.Employees)
                .HasForeignKey(e => e.id_employee);

            modelBuilder.Entity<Orders>()
                .Property(e => e.cost_order)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Orders>()
                .Property(e => e.patient_FIO)
                .IsUnicode(false);

            modelBuilder.Entity<Orders>()
                .Property(e => e.employee_FIO)
                .IsUnicode(false);

            modelBuilder.Entity<Orders>()
                .Property(e => e.service_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Patients>()
                .Property(e => e.FIO)
                .IsUnicode(false);

            modelBuilder.Entity<Patients>()
                .Property(e => e.Passport)
                .IsUnicode(false);

            modelBuilder.Entity<Patients>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Patients>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.Patients)
                .HasForeignKey(e => e.id_patient);

            modelBuilder.Entity<Roles>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.Roles)
                .HasForeignKey(e => e.id_Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Services>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Services>()
                .Property(e => e.Cost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Services>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.Services)
                .HasForeignKey(e => e.id_services);
        }
    }
}
