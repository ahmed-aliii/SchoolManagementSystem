using School.Domain;
using System.ComponentModel.DataAnnotations;

namespace School.Presentation
{
    public class UpdateDepartmentVM : DepartmentBase
    {
        [Required]
        public int Id { get; set; }

    }
}
