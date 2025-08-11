using System.ComponentModel.DataAnnotations;
using TeachPortal.Domain.Entities;

namespace TeachersPortal.Api.Domain.Entities
{
    // Student entity
    public class Student : BaseEntity
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

        [MaxLength(10)]
        public Gender Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        [MaxLength(250)]
        public string Address { get; set; }

        public int CountryId { get; set; }

        // Relationships
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
        
    }

}
