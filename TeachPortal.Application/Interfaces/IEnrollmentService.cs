using System;

namespace TeachPortal.Application.Interfaces
{
    public interface IEnrollmentService
    {
        Task EnrollStudentInCourse(int studentId, int courseId);
        Task AssignTeacherToCourse(int teacherId, int courseId);
    }
}
