using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain
{
    public abstract class DepartmentBase
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
