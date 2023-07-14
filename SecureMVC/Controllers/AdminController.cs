using HomeWork.Data;
using HomeWork.Models;
using HomeWork.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HomeWork.Controllers;

[Authorize]
public class AdminController : Controller {

    private IStoreRepository repository;

    public AdminController(IStoreRepository repository) {
        this.repository = repository;
    }

    public IActionResult Index() {

        var faculties = repository.Faculties;
        var students = repository.Students;

        Students_Faculties_ViewModel viewModel = new Students_Faculties_ViewModel() {
            Faculties = faculties,
            Students = students
        };

        return View(viewModel);
    }


    // Faculties
    [HttpGet]
    [Authorize(Roles = "FacultiesAdmin")]
    public IActionResult EditFaculty(int facultyId) {
        var faculty = repository.Faculties.FirstOrDefault(faculty => faculty.FacultyId == facultyId);
        return View(faculty);
    }

    [HttpPost]
    [Authorize(Roles = "FacultiesAdmin")]
    public async Task<IActionResult> EditFaculty(Faculty faculty) {
        if (ModelState.IsValid) {
            await repository.SaveFacultyAsync(faculty);
            TempData["message"] = "Faculty updated!";
            return RedirectToAction("Index");
        }
        else {
            return View(faculty);
        }
    }


    [HttpGet]
    [Authorize(Roles = "FacultiesAdmin")]
    public IActionResult CreateFaculty() {
        return View("EditFaculty", new Faculty());
    }



    // Imperative
    [HttpPost]
    public async Task<IActionResult> DeleteFaculty(int facultyId) {
        if (User.IsInRole("FacultiesAdmin")) {

            Faculty deletedFaculty = await repository.DeleteFacultyAsync(facultyId);
            if (deletedFaculty != null) {
                TempData["message"] = $"{deletedFaculty.FacultyName} was deleted";
            }
            return RedirectToAction("Index");
        } else {
            return null;
        }
    }



    // Students
    [HttpGet]
    [Authorize(Roles = "StudentsAdmin")]
    public IActionResult EditStudent(int studentId) {
        var student = repository.Students.FirstOrDefault(student => student.StudentId == studentId);
        return View(student);
    }

    [HttpPost]
    [Authorize(Roles = "StudentsAdmin")]
    public async Task<IActionResult> EditStudent(Student student) {
        if (ModelState.IsValid) {
            await repository.SaveStudentAsync(student);
            TempData["message"] = "Student updated!";
            return RedirectToAction("Index");
        }
        else {
            return View(student);
        }
    }


    [HttpGet]
    [Authorize(Roles = "StudentsAdmin")]
    public IActionResult CreateStudent() {
        return View("EditStudent", new Student());
    }


    [HttpPost]
    [Authorize(Roles = "StudentsAdmin")]
    public async Task<IActionResult> DeleteStudent(int studentId) {
        Student deletedStudent = await repository.DeleteStudentAsync(studentId);
        if (deletedStudent != null) {
            TempData["message"] = $"{deletedStudent.Surname} was deleted";
        }
        return RedirectToAction("Index");
    }

}
