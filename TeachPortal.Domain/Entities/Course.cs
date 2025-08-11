using System.ComponentModel.DataAnnotations;

namespace TeachersPortal.Api.Domain.Entities
{
    public class Course : BaseEntity
    {
        [Required]
        public string CourseName { get; set; }
        [Required]
        [StringLength(500)]
        public string CourseDescription { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
        public ICollection<TeacherCourse> TeacherCourses { get; set; } = new List<TeacherCourse>();
    }
}
