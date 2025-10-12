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
            // Table name (optional, defaults to "Students")
            builder.ToTable("Students");

            // Primary key
            builder.HasKey(s => s.Id);

            // Properties
            builder.Property(s => s.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(s => s.SSN)
                .IsRequired();

            builder.Property(s => s.Email)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(s => s.Age)
                   .IsRequired();

            builder.Property(s => s.Address)
                   .IsRequired();

            builder.Property(s => s.ImageURL)
                   .HasMaxLength(250);

        }
    }
}
