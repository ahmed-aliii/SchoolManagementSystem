using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain
{
    public class StudentCourse
    {
        public int Grade { get; set; }

        #region StudentCourse M----1 Student
        public virtual Student Student { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }
        #endregion

        #region StudentCourse M----1 Course
        public virtual Course Course { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        #endregion
    }
}
