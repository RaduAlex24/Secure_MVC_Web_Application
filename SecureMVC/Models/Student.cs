using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeWork.Models {
    public class Student {

        public int StudentId { get; set; }

        [Required(ErrorMessage ="Please enter a name")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Please enter a surname")]
        public string Surname { get; set; }


        [Required(ErrorMessage = "Please enter a price")]
        [Range(1,10,ErrorMessage ="Please enter a grate between 1 and 10")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal AverageGrade { get; set; }


        [Required(ErrorMessage = "Please enter a faculty id")]
        [ForeignKey("Faculty")]
        public int FacultyId { get; set; }


    }
}
