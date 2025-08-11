using TeachersPortal.Api.Domain.Entities;

namespace TeachersPortal.Application.Interfaces
{
    public interface ITeachersAppService
    {
        // Create a new teacher
        Task<Teacher> CreateTeacher(Teacher teacher);

        // Retrieve a teacher by ID
        Task<Teacher> GetTeacherById(int id);

        // Retrieve all teachers
        Task<IEnumerable<Teacher>> GetAllTeachers();

        // Update an existing teacher
        Task<Teacher> UpdateTeacher(Teacher teacher);

        // Delete a teacher by ID
        Task<bool> DeleteTeacher(int id);

        // Retrieve students taught by a specific teacher
        Task<IEnumerable<Teacher>> GetTeachersOfStudent(int studentId);


    }
}
