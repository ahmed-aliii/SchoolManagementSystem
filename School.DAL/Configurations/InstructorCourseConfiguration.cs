using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using School.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.DAL
{
    public class InstructorCourseConfiguration : IEntityTypeConfiguration<InstructorCourse>
    {
        public void Configure(EntityTypeBuilder<InstructorCourse> builder)
        {
            // Table name
            builder.ToTable("InstructorCourses");

            // ✅ Composite Primary Key
            builder.HasKey(ic => new { ic.InstructorId, ic.CourseId });

            // Properties
            builder.Property(ic => ic.RateHour)
                   .IsRequired();

            // ------------------ Relationships ------------------

            #region InstructorCourse M---1 Instructor
            builder.HasOne(ic => ic.Instructor)
                   .WithMany(i => i.InstructorCourses)
                   .HasForeignKey(ic => ic.InstructorId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region InstructorCourse M---1 Course
            builder.HasOne(ic => ic.Course)
                   .WithMany(c => c.InstructorCourses)
                   .HasForeignKey(ic => ic.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
