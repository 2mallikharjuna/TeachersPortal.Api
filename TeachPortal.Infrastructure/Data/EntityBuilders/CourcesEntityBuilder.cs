using Microsoft.EntityFrameworkCore;
using TeachersPortal.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TeachPortal.Infrastructure.Data.EntityBuilders
{
    public static class CourcesEntityBuilder
    {
        public static void Build(EntityTypeBuilder<Course> entity)
        {
            entity.ToTable("tbl_Cources");

            // Primary Key
            entity.HasKey(e => e.Id);

            // Properties
            entity.Property(e => e.CourseName)
                .IsRequired();

            entity.Property(e => e.CourseDescription)
                .IsRequired()
                .HasMaxLength(500);

            entity.Property(e => e.CreatedDate)
                .IsRequired();

            entity.Property(e => e.CreatedBy)
                .IsRequired();

            entity.Property(e => e.EndDate);

            // Relationships: StudentCourses (many-to-many)
            entity.HasMany(e => e.StudentCourses)
                .WithOne(sc => sc.Course)
                .HasForeignKey(sc => sc.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationships: TeacherCourses (many-to-many)
            entity.HasMany(e => e.TeacherCourses)
                .WithOne(tc => tc.Course)
                .HasForeignKey(tc => tc.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
