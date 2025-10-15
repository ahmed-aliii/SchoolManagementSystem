using Microsoft.EntityFrameworkCore;
using School.BLL;
using School.DAL;
using System.ComponentModel.DataAnnotations;

namespace School.Presentation
{
    public class UniqueCourseNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var context = new SchoolDB();
            if (context == null)
                return new ValidationResult("Internal error: Database context not available.");

            var courseVM = (UpdateCourseVM)validationContext.ObjectInstance;
            string courseName = value.ToString().Trim().ToLower();

            var existingCourse = context.Courses
                .AsNoTracking()
                .FirstOrDefault(c => c.Name.ToLower() == courseName);

            if (existingCourse != null && existingCourse.Id != courseVM.Id)
            {
                return new ValidationResult($"A course with the name '{courseVM.Name}' already exists.");
            }

            return ValidationResult.Success;
        }
    }
}
