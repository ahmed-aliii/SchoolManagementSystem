using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string ImageURL { get; set; }


        #region Student M---1 Department
        public Department Department { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        #endregion

        #region Student 1---M StudentCourse
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
        #endregion
    }
}
