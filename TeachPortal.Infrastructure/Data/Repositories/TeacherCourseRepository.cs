using TeachPortal.Domain.Repositories;
using TeachersPortal.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using TeachPortal.Domain.Repositories.Specifications;
using TeachPortal.Infrastructure.Data.Core;

namespace TeachPortal.Infrastructure.Data.Repositories
{
    public class TeacherCourseRepository : BaseRepository<TeacherCourse, int>, ITeacherCourseRepository
    {
        protected TeacherCourseRepository(IDatabaseContext dbContext) : base(dbContext)
        {

        }
        public async Task<bool> IsTeacherAssignedAsync(int teacherId, int courseId)
        {
            var specification = TeacherCourceSpecFactory.TeacherAssignedToCourseSpec(teacherId, courseId);
            return await AnyAsync(specification);
        }



        public async Task AssignTeacherAsync(int teacherId, int courseId)
        {
            if (!await IsTeacherAssignedAsync(teacherId, courseId))
            {
                await AddAsync(new TeacherCourse { TeacherId = teacherId, CourseId = courseId });
            }
        }
    }
}
