using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Cardio101.Models;

namespace Cardio101.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Device>()
                .HasIndex(b => b.SerialNumber)
                .IsUnique();

            modelBuilder.Entity<Study>()
                .HasOne(p => p.Device)
                .WithMany(b => b.Studies)
                .IsRequired();

            modelBuilder.Entity<Study>()
                .HasOne(p => p.Patient)
                .WithMany(b => b.Studies)
                .IsRequired();

            modelBuilder.Entity<Study>()
                .Property(b => b.LowHeartRate)
                .HasDefaultValue(true);

            modelBuilder.Entity<Study>()
                .Property(b => b.HighHeartRate)
                .HasDefaultValue(true);
        }
        public DbSet<Cardio101.Models.Device> Device { get; set; }
        public DbSet<Cardio101.Models.Patient> Patient { get; set; }
        public DbSet<Cardio101.Models.Study> Study { get; set; }
        public DbSet<Cardio101.Models.DeviceRecords> DeviceRecords { get; set; }
    }
}