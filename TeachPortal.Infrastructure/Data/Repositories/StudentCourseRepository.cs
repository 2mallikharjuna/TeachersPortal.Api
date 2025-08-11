using System;
using TeachPortal.Domain.Repositories;
using TeachersPortal.Api.Domain.Entities;
using TeachPortal.Domain.Repositories.Specifications;
using TeachPortal.Infrastructure.Data.Core;

namespace TeachPortal.Infrastructure.Data.Repositories
{
    public class StudentCourseRepository : BaseRepository<StudentCourse, int>, IStudentCourseRepository
    {
        protected StudentCourseRepository(IDatabaseContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> IsStudentEnrolledAsync(int studentId, int courseId)
        {
            var specification = StudentCourseSpecFactory.StudentAssignedToCourseSpec(studentId, courseId);
            return await AnyAsync(specification);
        }

        public async Task EnrollStudentAsync(int studentId, int courseId)
        {
            if (!await IsStudentEnrolledAsync(studentId, courseId))
            {
                AddAsync(new StudentCourse { StudentId = studentId, CourseId = courseId });
            }
        }
    }
}
