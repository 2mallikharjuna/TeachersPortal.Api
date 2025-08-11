using Microsoft.Extensions.Logging;
using TeachersPortal.Application.Interfaces;
using TeachersPortal.Api.Domain.Entities;
using TeachersPortal.Api.Domain.Repositories;

namespace TeachersPortal.Application.Services
{
    public class TeachersAppService : ITeachersAppService
    {
        private readonly ILogger<TeachersAppService> _logger;
        private readonly ITeacherRepository _teacherRepository;
        
        public TeachersAppService(ILogger<TeachersAppService> logger,
            ITeacherRepository teacherRepo)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _teacherRepository = teacherRepo ?? throw new ArgumentNullException(nameof(teacherRepo));            
        }

        // Implements ITeachersAppService.CreateTeacher
        public async Task<Teacher> CreateTeacher(Teacher teacher)
        {
            if (teacher == null)
                throw new ArgumentNullException(nameof(teacher));

            await _teacherRepository.AddAsync(teacher);
            return teacher;
        }

        // Implements ITeachersAppService.GetTeacherById
        public async Task<Teacher> GetTeacherById(int id)
        {
            var teacher = await _teacherRepository.GetAsync(id);
            return teacher;
        }

        // Implements ITeachersAppService.GetAllTeachers
        public async Task<IEnumerable<Teacher>> GetAllTeachers()
        {
            var teachers = await _teacherRepository.GetAllAsync();
            return teachers;
        }

        // Implements ITeachersAppService.UpdateTeacher
        public async Task<Teacher> UpdateTeacher(Teacher teacher)
        {
            if (teacher == null)
                throw new ArgumentNullException(nameof(teacher));

            var updatedTeacher = await _teacherRepository.UpdateAsync(teacher);
            return updatedTeacher;
        }

        // Implements ITeachersAppService.DeleteTeacher
        public async Task<bool> DeleteTeacher(int id)
        {
            var teacher = await _teacherRepository.GetAsync(id);
            if (teacher == null)
                return false;

            _teacherRepository.Remove(teacher);
            return true;
        }

        // Implements ITeachersAppService.GetTeachersOfStudent
        public async Task<IEnumerable<Teacher>> GetTeachersOfStudent(int studentId)
        {
            return await _teacherRepository.GetTeachersOfStudent(studentId);
        }
        
    }
}

