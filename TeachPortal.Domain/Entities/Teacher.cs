using TeachPortal.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace TeachersPortal.Api.Domain.Entities
{
    // Teacher entity
    public class Teacher : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        public Gender Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        [MaxLength(250)]
        public string Address { get; set; }

        // Relationships
        public ICollection<TeacherCourse> TeacherCourses { get; set; } = new List<TeacherCourse>();        
    }


}
