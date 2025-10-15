using School.BLL;
using System.ComponentModel.DataAnnotations;

namespace School.Presentation
{
    public class UniqueCreateCourseNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var courseVM = (CreateCourseVM)validationContext.ObjectInstance;
            var courseService = new CourseService();

            var exists = courseService.GetByNameAsync(courseVM.Name);

            if (exists != null)
                return new ValidationResult($"A course with the name '{courseVM.Name}' already exists.");

            return ValidationResult.Success;
        }
    }
}
