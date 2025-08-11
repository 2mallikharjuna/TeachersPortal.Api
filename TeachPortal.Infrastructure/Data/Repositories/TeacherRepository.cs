using TeachersPortal.Api.Domain.Entities;
using TeachersPortal.Api.Domain.Repositories;
using TeachPortal.Domain.Repositories.Specifications;
using TeachPortal.Infrastructure.Data.Core;

namespace TeachPortal.Infrastructure.Data.Repositories
{
    public class TeacherRepository : BaseRepository<Teacher, int>, ITeacherRepository
    {
        public TeacherRepository(IDatabaseContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Teacher>> GetTeachersOfStudent(int studentId)
        {
            var specification = TeacherSpecFactory.TeachersOfStudent(studentId);
            return await AllMatchingAsync(specification);
        }
    }
}

