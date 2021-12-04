using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cardio101.Models;

namespace Cardio101.Data
{
    public class Cardio101Context : DbContext
    {
        public Cardio101Context (DbContextOptions<Cardio101Context> options)
            : base(options)
        {
        }

        public DbSet<Cardio101.Models.Patient> Patient { get; set; }
    }
}
