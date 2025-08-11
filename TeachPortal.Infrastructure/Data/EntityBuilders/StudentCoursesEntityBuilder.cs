using Microsoft.EntityFrameworkCore;
using TeachersPortal.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TeachPortal.Infrastructure.Data.EntityBuilders
{
    public static class StudentCoursesEntityBuilder
    {
        public static void Build(EntityTypeBuilder<StudentCourse> entity)
        {
            // Map to table
            entity.ToTable("tbl_StudentCourse");

            // Configure composite primary key
            entity.HasKey(sc => new { sc.StudentId, sc.CourseId });

            // Configure relationships
            entity.HasOne(sc => sc.Student)
                  .WithMany(s => s.StudentCourses)
                  .HasForeignKey(sc => sc.StudentId);

            entity.HasOne(sc => sc.Course)
                  .WithMany(c => c.StudentCourses)
                  .HasForeignKey(sc => sc.CourseId);
        }
    }
}
