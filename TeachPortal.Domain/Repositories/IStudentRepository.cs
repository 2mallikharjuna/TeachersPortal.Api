using TeachersPortal.Api.Domain.Entities;
using TeachersPortal.Api.Domain.Repositories.Core;

namespace TeachersPortal.Api.Domain.Repositories
{
    public interface IStudentRepository : IRepository<Student, int>
    {        
        Task<IEnumerable<Student>> GetByTeacherId(int teacherId);
    }

}
