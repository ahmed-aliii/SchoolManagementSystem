using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain
{
    public class Instructor
    {
        public  int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }
        public string ImageURL { get; set; }
        public DateTime HireDate { get; set; }

        #region Instructor M---1 Department
        public virtual Department Department { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        #endregion

        #region Instructor 1---M InstructorCourse
        public ICollection<InstructorCourse> InstructorCourses { get; set; } = new List<InstructorCourse>();
        #endregion


    }
}
