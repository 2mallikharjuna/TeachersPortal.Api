using System;
using TeachersPortal.Api.Domain.Entities;
using TeachersPortal.Api.Domain.Core.FetchStrategy;
using TeachersPortal.Api.Domain.Core.Specifications;
using TeachersPortal.Api.Domain.Core.Specifications.Interfaces;

namespace TeachPortal.Domain.Repositories.Specifications
{
    public static class TeacherSpecFactory
    {
        public static ISpecification<Teacher> TeachersOfStudent(int studentId)
        {
            // Create the specification predicate
            var specification = new DirectSpecification<Teacher>(
                teacher => teacher.TeacherCourses.Any(tc => tc.Course.StudentCourses.Any(sc => sc.StudentId == studentId)));
            // Build fetch strategy for eager loading
            var fetchStrategy = new GenericFetchStrategy<Teacher>()
                .Include(nameof(Teacher.TeacherCourses))
                .Include($"{nameof(Teacher.TeacherCourses)}.{nameof(TeacherCourse.Course)}");


            specification.FetchStrategy = fetchStrategy;
            return specification;
        }
    }
}
