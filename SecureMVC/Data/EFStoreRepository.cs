using HomeWork.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Data {
    public class EFStoreRepository : IStoreRepository {
        private ApplicationDbContext context;

        public EFStoreRepository(ApplicationDbContext ctx) {
            context = ctx;
        }


        // Faculty
        public IQueryable<Faculty> Faculties {
            get { return context.Faculties; }
        }

        public async Task SaveFacultyAsync(Faculty faculty) {
            if(faculty.FacultyId == 0) {
                context.Faculties.Add(faculty);
            }
            else {
                Faculty entity = context.Faculties.FirstOrDefault(x => x.FacultyId == faculty.FacultyId);

                if (entity != null) {
                    entity.FacultyName = faculty.FacultyName;
                    entity.Specialization = faculty.Specialization;
                }
            }

            await context.SaveChangesAsync();
        }


        public async Task<Faculty> DeleteFacultyAsync(int facultyId) {
            Faculty? dbEntry =  context.Faculties.FirstOrDefault(x => x.FacultyId == facultyId);

            if (dbEntry != null) {
                context.Faculties.Remove(dbEntry);
                await context.SaveChangesAsync();
            }

            return dbEntry;
        }


        // Student
        public IQueryable<Student> Students {
            get { return context.Students; }
        }


        public async Task SaveStudentAsync(Student student) {
            if (student.StudentId == 0) {
                context.Students.Add(student);
            }
            else {
                Student entity = context.Students.FirstOrDefault(x => x.StudentId == student.StudentId);

                if (entity != null) {
                    entity.Name = student.Name;
                    entity.Surname = student.Surname;
                    entity.AverageGrade = student.AverageGrade;
                    entity.FacultyId = student.FacultyId;
                }
            }

            await context.SaveChangesAsync();


            // SQL injection
            //if (student.StudentId == 0) {

            //    string query = $"INSERT INTO Students(Name, Surname, AverageGrade, FacultyId) VALUES" +
            //        $" ('{student.Name}', '{student.Surname}', {student.AverageGrade}, {student.FacultyId})";

            //    context.Database.ExecuteSqlRaw(query);
            //}

        }


        public async Task<Student> DeleteStudentAsync(int studentId) {
            Student? dbEntry = context.Students.FirstOrDefault(x => x.StudentId == studentId);

            if (dbEntry != null) {
                context.Students.Remove(dbEntry);
                await context.SaveChangesAsync();
            }

            return dbEntry;
        }
    }
}
