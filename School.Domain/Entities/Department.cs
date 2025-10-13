using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manager { get; set; }
        public string Location { get; set; }

        #region Department 1----M Student
        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
        #endregion

        #region Department 1----M Instructor
        public virtual ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
        #endregion

    }
}
