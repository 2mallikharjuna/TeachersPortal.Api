using Microsoft.Extensions.Logging;
using TeachersPortal.Api.Domain.Entities;
using TeachersPortal.Api.Domain.Repositories;
using TeachersPortal.Application.Interfaces;

namespace TeachersPortal.Application.Services
{
    public class CoursesAppService : ICoursesAppService
    {
        private readonly ICoursesRepository _courcesRepository;
        private readonly ILogger<CoursesAppService> _logger;

        public CoursesAppService(ICoursesRepository courcesRepository, ILogger<CoursesAppService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _courcesRepository = courcesRepository ?? throw new ArgumentNullException(nameof(courcesRepository));
        }

        public async Task<Course> CreateCourseAsync(Course course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course));

            try
            {
                await _courcesRepository.AddAsync(course);
                _logger.LogInformation("Course created successfully: {CourseName}", course.CourseName);
                return course;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating course: {CourseName}", course?.CourseName);
                throw;
            }
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            try
            {
                var course = await _courcesRepository.GetAsync(id);
                if (course == null)
                {
                    _logger.LogWarning("Course not found for deletion: {CourseId}", id);
                    return false;
                }

                _courcesRepository.Remove(course);
                _logger.LogInformation("Course deleted successfully: {CourseId}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting course: {CourseId}", id);
                throw;
            }
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            try
            {
                var courses = await _courcesRepository.GetAllAsync();
                return courses;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all courses");
                throw;
            }
        }

        public async Task<Course> GetCourseByIdAsync(int courseId)
        {
            try
            {
                var course = await _courcesRepository.GetAsync(courseId);
                if (course == null)
                {
                    _logger.LogWarning("Course not found: {CourseId}", courseId);
                }
                return course;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving course by id: {CourseId}", courseId);
                throw;
            }
        }

        public IEnumerable<TeacherCourse> SelectAllTeacherCourses()
        {
            try
            {
                // Fix: Use repository's GetAllAsync and select TeacherCourses
                var coursesTask = _courcesRepository.GetAllAsync();
                coursesTask.Wait();
                var courses = coursesTask.Result;
                return courses.SelectMany(c => c.TeacherCourses ?? Enumerable.Empty<TeacherCourse>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all teacher courses");
                throw;
            }
        }

        public async Task<Course> UpdateCourseAsync(Course course)
        {
            return await UpdateCourseAsync(course);
        }

        public async Task<IEnumerable<Course>> GetAllCourcesOfStudent(int studentId)
        {
            try
            {
                return await _courcesRepository.GetCourcesOfStudent(studentId);
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving courses for student: {StudentId}", studentId);
                throw;
            }
        }

        public async Task<IEnumerable<Course>> GetAllCourcesOfTeacher(int teacherId)
        {
            try
            {
                return await _courcesRepository.GetCourcesOfTeacher(teacherId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving courses for student: {StudentId}", teacherId);
                throw;
            }
        }
    }
}
