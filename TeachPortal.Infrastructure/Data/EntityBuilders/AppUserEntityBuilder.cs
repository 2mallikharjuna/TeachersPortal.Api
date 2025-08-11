using TeachPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace TeachPortal.Infrastructure.Data.EntityBuilders
{
    public static class AppUserEntityBuilder
    {
        public static void Build(EntityTypeBuilder<AppUser> entity)
        {
            // Map to table
            entity.ToTable("tbl_AppUser");

            // Primary Key
            entity.HasKey(e => e.Id);

            // Properties
            entity.Property(e => e.Id)
                .IsRequired();

            entity.Property(e => e.TeacherId)
                .IsRequired();

            entity.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.HashPassword)
                .IsRequired()
                .HasMaxLength(256);

            entity.Property(e => e.CreatedDate)
                .IsRequired();

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100);

            entity.Property(e => e.EndDate);

            // Relationships
            entity.HasOne(e => e.Teacher)
                .WithMany()
                .HasForeignKey(e => e.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
