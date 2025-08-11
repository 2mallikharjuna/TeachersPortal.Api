using TeachersPortal.Api.Domain.Entities;
using TeachersPortal.Api.Domain.Repositories.Core;
using TeachersPortal.Api.Domain.Core.Specifications.Interfaces;

namespace TeachersPortal.Api.Domain.Repositories
{
    public interface ICoursesRepository : IRepository<Course, int>, ISpecifiable
    {
        Task<IEnumerable<Course>> GetCourcesOfTeacher(int teacherId);
        Task<IEnumerable<Course>> GetCourcesOfStudent(int studentId);
    }
}
