using System;
using TeachersPortal.Api.Domain.Core.FetchStrategy;
using TeachersPortal.Api.Domain.Core.Specifications;
using TeachersPortal.Api.Domain.Core.Specifications.Interfaces;
using TeachersPortal.Api.Domain.Entities;

namespace TeachPortal.Domain.Repositories.Specifications
{
    public static class TeacherCourceSpecFactory
    {
        public static ISpecification<TeacherCourse> TeacherAssignedToCourseSpec(int teacherId, int courseId)                
        {
            return new DirectSpecification<TeacherCourse>(tc => tc.TeacherId == teacherId && tc.CourseId == courseId)
            {
                FetchStrategy = new GenericFetchStrategy<TeacherCourse>()
                    .Include(nameof(TeacherCourse.Teacher))
                    .Include(nameof(TeacherCourse.Course))
            };
        }
    }
}
