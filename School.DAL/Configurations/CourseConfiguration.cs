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
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            // Table name
            builder.ToTable("Courses");

            // Primary Key
            builder.HasKey(c => c.Id);

            // Properties
            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Topic)
                   .HasMaxLength(100);

            builder.Property(c => c.Degree)
                   .IsRequired();

            builder.Property(c => c.MinDegree)
                   .IsRequired();

            // ------------------ Relationships ------------------

            #region Course 1---M StudentCourse
            builder.HasMany(c => c.StudentCourses)
                   .WithOne(sc => sc.Course)
                   .HasForeignKey(sc => sc.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Course 1---M InstructorCourse
            builder.HasMany(c => c.InstructorCourses)
                   .WithOne(ic => ic.Course)
                   .HasForeignKey(ic => ic.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
