using HomeWork.Data;
using HomeWork.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Controllers {
    public class HomeController : Controller {


        private IStoreRepository repository;
        public HomeController(IStoreRepository repo) {
            repository = repo;
        }



        public IActionResult Index() {
            ViewBag.Title = "Project";

            var faculties = repository.Faculties;
            var students = repository.Students;

            Students_Faculties_ViewModel viewModel = new Students_Faculties_ViewModel() {
                Faculties = faculties,
                Students = students
            };

            return View(viewModel);
        }
    }
}
