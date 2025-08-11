namespace TeachersPortal.Api.Domain.Entities
{
    // Join entity for Teacher-Course many-to-many relationship
    public class TeacherCourse : BaseEntity
    {
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
