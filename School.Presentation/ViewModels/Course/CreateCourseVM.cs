using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace School.Presentation
{
    public class CreateCourseVM
    {
        [Remote(action: "IsCourseNameUnique", controller: "Course", AdditionalFields = "Id", ErrorMessage = "A course with this name already exists.")]
        [Required(ErrorMessage = "Course name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Course name must be between 3 and 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Topic is required.")]
        [StringLength(100, ErrorMessage = "Topic cannot exceed 100 characters.")]
        public string Topic { get; set; }

        [Required(ErrorMessage = "Degree is required.")]
        [Range(1, 100, ErrorMessage = "Degree must be between 1 and 100.")]
        public int Degree { get; set; }

        [Required(ErrorMessage = "MinDegree is required.")]
        [Range(0, 99, ErrorMessage = "MinDegree must be between 0 and 99.")]
        [ValidCreateDegreeRange]
        public int MinDegree { get; set; }
    }
}
