using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Domain
{
    public class InstructorCourse
    {
        public int RateHour { get; set; }


        #region InstructorCourse M---1 Instructor
        public virtual Instructor Instructor { get; set; }

        [ForeignKey("Instructor")]
        public int InstructorId { get; set; }
        #endregion

        #region InstructorCourse M---1 Course
        public virtual Course Course { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        #endregion
    }
}
