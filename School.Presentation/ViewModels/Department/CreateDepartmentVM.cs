using System.ComponentModel.DataAnnotations;

namespace School.Presentation
{
    public class CreateDepartmentVM
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
        public string Name { get; set; }
       
        
        [Required(ErrorMessage = "Manager Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Manager Name must be between 3 and 50 characters")]
        public string Manager { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Location must be between 3 and 50 characters")]
        public string Location { get; set; }
    }
}
