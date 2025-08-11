using System;
using TeachersPortal.Api.Domain.Entities;
using TeachersPortal.Api.Domain.Core.Specifications.Interfaces;
using TeachersPortal.Api.Domain.Core.FetchStrategy;
using TeachersPortal.Api.Domain.Core.Specifications;
using TeachersPortal.Api.Domain.Core.Specification;

namespace TeachPortal.Domain.Repositories.Specifications
{
    public static class CourceSpecFactory
    {
        
        public static ISpecification<Course> GetCourcesOfStudent(int studentId)
        {
            // Create the specification predicate
            var specification = new DirectSpecification<Course>(
                course => course.StudentCourses.Any(sc => sc.StudentId == studentId));
            // Build fetch strategy for eager loading
            var fetchStrategy = new GenericFetchStrategy<Course>()
                .Include(nameof(Course.StudentCourses));

            specification.FetchStrategy = fetchStrategy;
            return specification;
        }

        public static ISpecification<Course> GetCourcesOfTeacher(object teacherId)
        {
            // Create the specification predicate
            var specification = new DirectSpecification<Course>(
                course => course.TeacherCourses.Any(tc => tc.TeacherId == Convert.ToInt32(teacherId)));
            // Build fetch strategy for eager loading
            var fetchStrategy = new GenericFetchStrategy<Course>()
                .Include(nameof(Course.TeacherCourses));

            specification.FetchStrategy = fetchStrategy;
            return specification;
        }
    }
}
