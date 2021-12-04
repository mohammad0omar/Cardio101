using Microsoft.EntityFrameworkCore;
using Cardio101.Models;

namespace Cardio101.Data
{
    public class MvcPatientContext : DbContext
    {
        public MvcPatientContext(DbContextOptions<MvcPatientContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patient { get; set; }
    }
}
