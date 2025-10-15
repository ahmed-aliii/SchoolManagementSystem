using System.ComponentModel.DataAnnotations;

namespace School.Presentation
{
    public class ValidDegreeRangeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var course = (UpdateCourseVM)validationContext.ObjectInstance;

            if (course.MinDegree >= course.Degree)
            {
                return new ValidationResult("MinDegree must be less than Degree.");
            }

            return ValidationResult.Success;
        }
    }
}
