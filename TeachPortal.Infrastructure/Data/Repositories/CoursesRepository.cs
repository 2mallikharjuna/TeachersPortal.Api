using Microsoft.EntityFrameworkCore;
using TeachersPortal.Api.Domain.Entities;
using TeachersPortal.Api.Domain.Repositories;
using TeachPortal.Domain.Repositories.Specifications;
using TeachPortal.Infrastructure.Data.Core;

namespace TeachPortal.Infrastructure.Data.Repositories
{
    public class CoursesRepository : BaseRepository<Course, int>, ICoursesRepository
    {
        public CoursesRepository(IDatabaseContext dbContext) : base(dbContext)
        {
        }



        public async Task<IEnumerable<Course>> GetCourcesOfTeacher(int teacherId)
        {
            var specification = CourceSpecFactory.GetCourcesOfTeacher(teacherId);
            return await AllMatchingAsync(specification);
        }

        public async Task<IEnumerable<Course>> GetCourcesOfStudent(int studentId)
        {
            var specification = CourceSpecFactory.GetCourcesOfTeacher(studentId);
            return await AllMatchingAsync(specification);
        }
    }
}
