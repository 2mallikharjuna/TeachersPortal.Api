using TeachersPortal.Api.Domain.Entities;
using TeachPortal.Application.DTOs.Requests;
using TeachPortal.Application.DTOs.Response;

namespace TeachersPortal.Application.Interfaces
{
    public interface IStudentsAppService
    {
        Task<StudentDto> CreateStudent(CreateStudent studentDto);
        Student UpdateStudent(int id, Student student);        
        bool DeleteStudent(int id);
        IEnumerable<Student> GetAllStudents();
        Student GetStudentById(int id);        
        Task<IEnumerable<StudentDto>> GetStudentsOfTeacher(int teacherId);        
    }
}
