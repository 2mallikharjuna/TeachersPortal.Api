using TeachersPortal.Api.Domain.Entities;

using TeachersPortal.Api.Domain.Repositories.Core;
using TeachersPortal.Api.Domain.Core.Specifications.Interfaces;

namespace TeachersPortal.Api.Domain.Repositories
{
    public interface ITeacherRepository : IRepository<Teacher, int>, ISpecifiable
    {
        Task<IEnumerable<Teacher>> GetTeachersOfStudent(int studentId);
    }
}
