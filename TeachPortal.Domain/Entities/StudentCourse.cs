namespace TeachersPortal.Api.Domain.Entities
{
    // Join entity for Student-Course many-to-many relationship
    public class StudentCourse : BaseEntity
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
