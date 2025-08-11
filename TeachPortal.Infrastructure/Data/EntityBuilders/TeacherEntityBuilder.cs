using TeachersPortal.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace TeachPortal.Infrastructure.Data.EntityBuilders
{
    public static class TeacherEntityBuilder
    {
        public static void Build(EntityTypeBuilder<Teacher> entity)
        {
            entity.ToTable("tbl_teachers");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .IsRequired();

            entity.Property(e => e.FirstName)
                .HasColumnName("first_name")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.LastName)
                .HasColumnName("last_name")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.Email)
                .HasColumnName("email")
                .IsRequired();

            entity.Property(e => e.Gender)
                .HasColumnName("gender")
                .HasMaxLength(10);

            entity.Property(e => e.DateOfBirth)
                .HasColumnName("date_of_birth");

            entity.Property(e => e.Address)
                .HasColumnName("address")
                .HasMaxLength(250);

            entity.Property(e => e.CreatedDate)
                .HasColumnName("created_date")
                .IsRequired();

            entity.Property(e => e.CreatedBy)
                .HasColumnName("created_by");

            entity.Property(e => e.EndDate)
                .HasColumnName("end_date");

            entity.HasMany(e => e.TeacherCourses)
                .WithOne(tc => tc.Teacher)
                .HasForeignKey(tc => tc.TeacherId);
        }
    }
}
