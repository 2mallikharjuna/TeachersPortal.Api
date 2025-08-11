using TeachersPortal.Api.Domain.Entities;
using TeachersPortal.Api.Domain.Repositories;
using Microsoft.Extensions.Logging;
using TeachPortal.Application.DTOs.Response;
using TeachPortal.Application.DTOs.Requests;
using TeachersPortal.Application.Interfaces;

namespace TeachersPortal.Application.Services
{
    public class StudentsAppService : IStudentsAppService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ILogger<StudentsAppService> _logger;

        public StudentsAppService(IStudentRepository studentRepository, ILogger<StudentsAppService> logger)
        {
            _studentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Student CreateStudent(Student student)
        {
            if (student == null)
            {
                _logger.LogError("CreateStudent failed: student is null.");
                throw new ArgumentNullException(nameof(student));
            }

            try
            {
                _studentRepository.Add(student);
                return student;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a student.");
                throw;
            }
        }

        public bool DeleteStudent(int id)
        {
            try
            {
                var student = _studentRepository.Get(id);
                if (student == null)
                {
                    _logger.LogWarning("DeleteStudent failed: Student with ID {StudentId} not found.", id);
                    return false;
                }

                _studentRepository.Remove(student);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the student with ID {StudentId}.", id);
                throw;
            }
        }

        public IEnumerable<Student> GetAllStudents()
        {
            try
            {
                return _studentRepository.GetAllAsync().Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all students.");
                throw;
            }
        }

        public Student GetStudentById(int id)
        {
            try
            {
                var student = _studentRepository.GetAsync(id).Result;
                if (student == null)
                {
                    _logger.LogWarning("GetStudentById failed: Student with ID {StudentId} not found.", id);
                }
                return student;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the student with ID {StudentId}.", id);
                throw;
            }
        }

        public Student UpdateStudent(int id, Student student)
        {
            if (student == null)
            {
                _logger.LogError("UpdateStudent failed: student is null.");
                throw new ArgumentNullException(nameof(student));
            }

            try
            {
                var existingStudent = _studentRepository.Get(id);
                if (existingStudent == null)
                {
                    _logger.LogWarning("UpdateStudent failed: Student with ID {StudentId} not found.", id);
                    throw new InvalidOperationException($"Student with ID {id} not found.");
                }

                existingStudent.FirstName = student.FirstName;
                existingStudent.LastName = student.LastName;
                existingStudent.Email = student.Email;
                existingStudent.Gender = student.Gender;
                existingStudent.DateOfBirth = student.DateOfBirth;
                existingStudent.Address = student.Address;
                existingStudent.CountryId = student.CountryId;

                return existingStudent;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the student with ID {StudentId}.", id);
                throw;
            }
        }
        public async Task<StudentDto> CreateStudent(CreateStudent dto)
        {
            var student = new Student
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email
            };

            await _studentRepository.AddAsync(student);

            return new StudentDto(student.Id, student.FirstName, student.LastName, student.Email);
        }

        public async Task<IEnumerable<StudentDto>> GetStudentsOfTeacher(int teacherId)
        {
            var students = await _studentRepository.GetByTeacherId(teacherId);
            return students.Select(s => new StudentDto(s.Id, s.FirstName, s.LastName, s.Email));
        }

        
    }
}
