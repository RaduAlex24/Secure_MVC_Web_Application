using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeWork.Models {
    public class Faculty {

        public int FacultyId { get; set; }


        [Required(ErrorMessage = "Please enter a faculty name")]
        public string FacultyName { get; set; }

        [Required(ErrorMessage = "Please enter a faculty specialization name")]
        public string Specialization { get; set; }

    }
}
