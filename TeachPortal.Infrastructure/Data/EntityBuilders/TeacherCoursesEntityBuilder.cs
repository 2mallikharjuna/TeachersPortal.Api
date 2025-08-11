using Microsoft.EntityFrameworkCore;
using TeachersPortal.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TeachPortal.Infrastructure.Data.EntityBuilders
{
    public static class TeacherCoursesEntityBuilder
    {
        public static void Build(EntityTypeBuilder<TeacherCourse> entity)
        {
            // Map to table
            entity.ToTable("tbl_TeacherCourse");

            // Configure primary key and properties
            entity.HasKey(tc => tc.Id);

            entity.Property(tc => tc.TeacherId)
                .IsRequired();

            entity.Property(tc => tc.CourseId)
                .IsRequired();

            entity.HasOne(tc => tc.Teacher)
                .WithMany(t => t.TeacherCourses)
                .HasForeignKey(tc => tc.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(tc => tc.Course)
                .WithMany(c => c.TeacherCourses)
                .HasForeignKey(tc => tc.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
