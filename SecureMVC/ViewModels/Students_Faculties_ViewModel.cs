using HomeWork.Models;

namespace HomeWork.ViewModels {
    public class Students_Faculties_ViewModel {

        public IEnumerable<Student> Students { get; set; } = Enumerable.Empty<Student>();

        public IEnumerable<Faculty> Faculties { get; set; } = Enumerable.Empty<Faculty>();

    }
}
