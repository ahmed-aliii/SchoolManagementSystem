using School.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Presentation.ViewModels.Student
{

    public class CreateStudentVM
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email format")]
        public string Email { get; set; }


        [Range(16, 100, ErrorMessage = "Age must be between 16 and 100")]
        public int Age { get; set; }


        [Required(ErrorMessage = "Address is required")]
        [StringLength(100, ErrorMessage = "Address cannot exceed 100 characters")]
        public string Address { get; set; }


        [Required(ErrorMessage = "Image URL is required")]
        public string ImageURL { get; set; } = "avatar.png";


        [Required(ErrorMessage = "Department is required")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }


        public List<Department> Departments { get; set; } = new List<Department>();
    }
}
