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
    public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            // Table name
            builder.ToTable("Instructors");

            // Primary key
            builder.HasKey(i => i.Id);

            // Properties
            builder.Property(i => i.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(i => i.Address)
                   .HasMaxLength(250);

            builder.Property(i => i.Age)
                   .IsRequired();

            builder.Property(i => i.Salary)
                   .HasColumnType("decimal(18,2)") // 👈 precise type for money
                   .IsRequired();

            builder.Property(i => i.ImageURL)
                   .HasMaxLength(500);

            builder.Property(i => i.HireDate)
                   .HasColumnType("date") // 👈 store only date part
                   .IsRequired();

            // ------------------ Relationships ------------------

            #region Instructor M---1 Department
            builder.HasOne(i => i.Department)
                   .WithMany(d => d.Instructors)
                   .HasForeignKey(i => i.DepartmentId)
                   .OnDelete(DeleteBehavior.Restrict); // avoid deleting instructors when a department is removed
            #endregion

            #region Instructor 1---M InstructorCourse
            builder.HasMany(i => i.InstructorCourses)
                   .WithOne(ic => ic.Instructor)
                   .HasForeignKey(ic => ic.InstructorId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
