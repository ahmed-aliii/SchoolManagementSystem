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
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            // Table name
            builder.ToTable("Students");

            // Primary Key
            builder.HasKey(s => s.Id);

            //Properties
            builder.Property(s => s.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(s => s.Email)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(s => s.Age)
                   .IsRequired();

            builder.Property(s => s.Address)
                   .HasMaxLength(250);

            builder.Property(s => s.ImageURL)
                   .HasMaxLength(500);

            // Relationships ----------------------------

            #region Student M---1 Department
            builder.HasOne(s => s.Department)
                   .WithMany(d => d.Students)
                   .HasForeignKey(s => s.DepartmentId)
                   .OnDelete(DeleteBehavior.Restrict); 
            #endregion

            #region Student 1---M StudentCourse
            builder.HasMany(s => s.StudentCourses)
                   .WithOne(sc => sc.Student)
                   .HasForeignKey(sc => sc.StudentId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
