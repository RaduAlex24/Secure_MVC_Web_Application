using HomeWork.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Data {
    public class SeedData {

        public static void EnsurePopulated(IApplicationBuilder app) {
            ApplicationDbContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (context.Database.GetPendingMigrations().Any()) {
                context.Database.Migrate();
            }

            if (!context.Faculties.Any()) {
                context.Faculties.AddRange(
                new Faculty {
                    FacultyName = "ASE",
                    Specialization = "CSIE",
                },
                new Faculty {
                    FacultyName = "Politehnica",
                    Specialization = "Automatica",
                }
                );

                context.SaveChanges();
            }


            if (!context.Students.Any()) {
                context.Students.AddRange(
                new Student {
                    Name = "Radu",
                    Surname = "Alexnadru",
                    AverageGrade = 8.5m,
                    FacultyId = 0
                },
                new Student {
                    Name = "Ilie",
                    Surname = "Andrei",
                    AverageGrade = 10m,
                    FacultyId = 0
                },
                new Student {
                    Name = "Dinu",
                    Surname = "Alina",
                    AverageGrade = 9.5m,
                    FacultyId = 1
                }
                );

                context.SaveChanges();
            }
        }

    }
}
