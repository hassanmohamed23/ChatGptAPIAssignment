using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System;

namespace ChatGPTAssignmentAPI.Models
{
    public class StudentEntityConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Student");
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).ValueGeneratedOnAdd();
            builder.Property(i => i.Name).IsRequired().HasMaxLength(100);
            builder.Property(i => i.Subject).IsRequired().HasMaxLength(100);
            builder.Property(i => i.Grade).IsRequired();


        }
    }
}
