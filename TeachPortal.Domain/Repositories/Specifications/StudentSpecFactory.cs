using TeachersPortal.Api.Domain.Entities;
using TeachersPortal.Api.Domain.Core.FetchStrategy;
using TeachersPortal.Api.Domain.Core.Specifications;
using TeachersPortal.Api.Domain.Core.Specifications.Interfaces;

namespace TeachPortal.Domain.Repositories.Specifications
{
    public static class StudentSpecFactory
    {
        public static ISpecification<Student> StudentsOfTeacher(int teacherId)
        {
            var spec = new DirectSpecification<Student>(s =>
                s.StudentCourses.Any(sc =>
                    sc.Course.TeacherCourses.Any(tc =>
                        tc.TeacherId == teacherId)));

            spec.FetchStrategy = new GenericFetchStrategy<Student>()
                .Include(s => s.StudentCourses)
                .Include("StudentCourses.Course");

            return spec;
        }
    }
}
