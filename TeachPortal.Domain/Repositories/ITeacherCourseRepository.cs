using System;
using TeachersPortal.Api.Domain.Entities;
using TeachersPortal.Api.Domain.Repositories.Core;

namespace TeachPortal.Domain.Repositories
{
    public interface ITeacherCourseRepository : IRepository<TeacherCourse, int>
    {
    }
}
