using TeachersPortal.Api.Domain.Core.Specifications.Interfaces;
using TeachersPortal.Api.Domain.Entities;
using TeachersPortal.Api.Domain.Repositories;
using TeachPortal.Domain.Repositories.Specifications;
using TeachPortal.Infrastructure.Data.Core;

namespace TeachPortal.Infrastructure.Data.Repositories
{
    public class StudentRepository : BaseRepository<Student, int>, IStudentRepository
    {
        public StudentRepository(IDatabaseContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Student>> GetByTeacherId(int teacherId)
        {
            var specification = StudentSpecFactory.StudentsOfTeacher(teacherId);
            return await AllMatchingAsync(specification);
        }
    }

}
