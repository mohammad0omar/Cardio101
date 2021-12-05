using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Cardio101.Data;
using System;
using System.Linq;

namespace Cardio101.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any movies.
                if (context.Patient.Any())
                {
                    return;   // DB has been seeded
                }

                context.Patient.AddRange(
                    new Patient
                    {
                        Name = "Harry Met Sally"
                    },

                    new Patient
                    {
                        Name = "Ghostbusters " 
                    },

                    new Patient
                    {
                        Name = "Ghostbusters 2"
                    },

                    new Patient
                    {
                        Name = "Rio Bravo"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}