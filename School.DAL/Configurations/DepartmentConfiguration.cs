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
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            // Table name
            builder.ToTable("Departments");

            // Primary Key
            builder.HasKey(d => d.Id);

            // Properties
            builder.Property(d => d.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(d => d.Manager)
                   .HasMaxLength(100);

            builder.Property(d => d.Location)
                   .HasMaxLength(150);

            // ------------------ Relationships ------------------

            #region Department 1---M Student
            builder.HasMany(d => d.Students)
                   .WithOne(s => s.Department)
                   .HasForeignKey(s => s.DepartmentId)
                   .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Department 1---M Instructor
            builder.HasMany(d => d.Instructors)
                   .WithOne(i => i.Department)
                   .HasForeignKey(i => i.DepartmentId)
                   .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
}
