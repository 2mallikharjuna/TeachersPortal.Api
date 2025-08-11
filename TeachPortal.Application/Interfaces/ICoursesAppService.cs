using TeachersPortal.Api.Domain.Entities;

namespace TeachersPortal.Application.Interfaces
{
    public interface ICoursesAppService
    {
        // Create a new course
        Task<Course> CreateCourseAsync(Course course);

        // Retrieve a course by ID
        Task<Course> GetCourseByIdAsync(int id);

        // Retrieve all courses
        Task<IEnumerable<Course>> GetAllCoursesAsync();

        // Update an existing course
        Task<Course> UpdateCourseAsync(Course course);

        // Delete a course by ID
        Task<bool> DeleteCourseAsync(int id);

        Task<IEnumerable<Course>> GetAllCourcesOfStudent(int studentId);
        Task<IEnumerable<Course>> GetAllCourcesOfTeacher(int teacherId);
    }
}

