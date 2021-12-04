using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cardio101.Models;

namespace Cardio101.Data
{
    public class Devices101Context : DbContext
    {
        public Devices101Context (DbContextOptions<Devices101Context> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>()
                .HasIndex(b => b.SerialNumber)
                .IsUnique();
        }
        public DbSet<Cardio101.Models.Device> Device { get; set; }
    }
}
