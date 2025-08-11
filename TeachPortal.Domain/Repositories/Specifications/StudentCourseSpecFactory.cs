using System;
using TeachersPortal.Api.Domain.Entities;
using TeachersPortal.Api.Domain.Core.FetchStrategy;
using TeachersPortal.Api.Domain.Core.Specifications;
using TeachersPortal.Api.Domain.Core.Specifications.Interfaces;

namespace TeachPortal.Domain.Repositories.Specifications
{
    public static class StudentCourseSpecFactory
    {
        public static ISpecification<StudentCourse> StudentAssignedToCourseSpec(int teacherId, int courseId)
        {
            return new DirectSpecification<StudentCourse>(sc => sc.StudentId == teacherId && sc.CourseId == courseId)
            {
                FetchStrategy = new GenericFetchStrategy<StudentCourse>()
                    .Include(nameof(StudentCourse.Student))
                    .Include(nameof(StudentCourse.Course))
            };
        }
    }
}
