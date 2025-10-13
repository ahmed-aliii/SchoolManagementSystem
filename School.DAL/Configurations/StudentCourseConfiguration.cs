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
    public class StudentCourseConfiguration : IEntityTypeConfiguration<StudentCourse>
    {
        public void Configure(EntityTypeBuilder<StudentCourse> builder)
        {
            // Table name
            builder.ToTable("StudentCourses");

            // ✅ Composite Primary Key
            builder.HasKey(sc => new { sc.StudentId, sc.CourseId });

            // Properties
            builder.Property(sc => sc.Grade)
                   .IsRequired();

            // ------------------ Relationships ------------------

            #region StudentCourse M---1 Student
            builder.HasOne(sc => sc.Student)
                   .WithMany(s => s.StudentCourses)
                   .HasForeignKey(sc => sc.StudentId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region StudentCourse M---1 Course
            builder.HasOne(sc => sc.Course)
                   .WithMany(c => c.StudentCourses)
                   .HasForeignKey(sc => sc.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
