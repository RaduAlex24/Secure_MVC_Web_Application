using HomeWork.Models;

namespace HomeWork.Data {
    public interface IStoreRepository {

        IQueryable<Student> Students { get; }
        IQueryable<Faculty> Faculties { get; }

        Task SaveFacultyAsync(Faculty faculty); 
        Task SaveStudentAsync(Student student);

        Task<Faculty> DeleteFacultyAsync(int facultyId);
        Task<Student> DeleteStudentAsync(int studentId);

    }

}
