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
               InitializeDevices(context);
               InitializePatients(context);

               context.SaveChanges();
            }
        }

        private static void InitializePatients(ApplicationDbContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }
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
        }
        private static void InitializeDevices(ApplicationDbContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (context.Device.Any())
            {
                return;   // DB has been seeded
            }

            context.Device.AddRange(
                new Device
                {
                    Name = "Cardio Heart 01",
                    SerialNumber = "Card01555896658H"
                },
                 new Device
                 {
                     Name = "Cardio Blood 021",
                     SerialNumber = "Card0144447859566B"
                 },
                  new Device
                  {
                      Name = "J&J Heart 188",
                      SerialNumber = "J05845454JJ54548H"
                  }

            );
        }
    }
}