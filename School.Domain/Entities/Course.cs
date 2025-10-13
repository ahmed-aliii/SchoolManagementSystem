using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Topic { get; set; }
        public int Degree { get; set; }
        public int MinDegree { get; set; }

        #region Course 1---M StudentCourse
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
        #endregion

        #region Course 1---M InstructorCourse
        public ICollection<InstructorCourse> InstructorCourses { get; set; } = new List<InstructorCourse>();

        #endregion
    }
}
