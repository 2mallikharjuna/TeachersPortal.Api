using TeachPortal.Application.Interfaces;
using TeachersPortal.Api.Domain.Repositories;
using TeachersPortal.Api.Domain.Entities;

public class EnrollmentService : IEnrollmentService
{
    ICoursesRepository _courcesRepository;
    public EnrollmentService(ICoursesRepository courcesRepository)
    {
        _courcesRepository = courcesRepository ?? throw new ArgumentNullException(nameof(courcesRepository));
    }

    public async Task AssignTeacherToCourse(int teacherId, int courseId)
    {
        var course = await _courcesRepository.GetAsync(courseId);
        if (course == null)
            throw new ArgumentException($"Course with ID {courseId} not found.");

        if (course.TeacherCourses == null)
            course.TeacherCourses = new List<TeacherCourse>();

        // Check if already assigned
        if (course.TeacherCourses.Any(tc => tc.TeacherId == teacherId))
            return;

        course.TeacherCourses.Add(new TeacherCourse { TeacherId = teacherId, CourseId = courseId });
        await _courcesRepository.UpdateAsync(course);
    }

    public async Task EnrollStudentInCourse(int studentId, int courseId)
    {
        var course = await _courcesRepository.GetAsync(courseId);
        if (course == null)
            throw new ArgumentException($"Course with ID {courseId} not found.");

        if (course.StudentCourses == null)
            course.StudentCourses = new List<StudentCourse>();

        // Check if already enrolled
        if (course.StudentCourses.Any(sc => sc.StudentId == studentId))
            return;

        course.StudentCourses.Add(new StudentCourse { StudentId = studentId, CourseId = courseId });
        await _courcesRepository.UpdateAsync(course);
    }
}
