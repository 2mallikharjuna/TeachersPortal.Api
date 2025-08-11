using TeachersPortal.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace TeachPortal.Infrastructure.Data.EntityBuilders
{
    public static class StudentEntityBuilder
    {
        public static void Build(EntityTypeBuilder<Student> entity)
        {
            entity.ToTable("tbl_Students");

            // Key
            entity.HasKey(e => e.Id);

            // Properties
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Email)
                .IsRequired();

            entity.Property(e => e.Gender)
                .HasMaxLength(10);

            entity.Property(e => e.DateOfBirth)
                .IsRequired();

            entity.Property(e => e.Address)
                .HasMaxLength(250);

            entity.Property(e => e.CountryId)
                .IsRequired();

            entity.Property(e => e.CreatedDate)
                .IsRequired();

            entity.Property(e => e.CreatedBy)
                .IsRequired();

            entity.Property(e => e.EndDate);

            // Relationships
            entity.HasMany(e => e.StudentCourses)
                .WithOne(sc => sc.Student)
                .HasForeignKey(sc => sc.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
