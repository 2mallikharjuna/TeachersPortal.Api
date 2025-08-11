using System;
using TeachPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using TeachersPortal.Api.Domain.Entities;
using TeachPortal.Infrastructure.Data.Core;
using TeachPortal.Infrastructure.Data.EntityBuilders;

namespace TeachPortal.Api.DatContext
{
    public class TpApiDbContext : BaseDataContext, ITpApiDbContext
    { 
        public TpApiDbContext(DbContextOptions<TpApiDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply configurations for entity
            modelBuilder.Entity<Student>(StudentEntityBuilder.Build);
            modelBuilder.Entity<Teacher>(TeacherEntityBuilder.Build);
            modelBuilder.Entity<Course>(CourcesEntityBuilder.Build);
            modelBuilder.Entity<StudentCourse>(StudentCoursesEntityBuilder.Build);
            modelBuilder.Entity<TeacherCourse>(TeacherCoursesEntityBuilder.Build);
            modelBuilder.Entity<AppUser>(AppUserEntityBuilder.Build);
            IList<Student> defaultStudents = new List<Student>();

            // Add default student records
            AddDefaultStudents(modelBuilder);

            modelBuilder.Entity<Student>().HasData(defaultStudents);

        }
        private void AddDefaultStudents(ModelBuilder modelBuilder)
        {
            IList<Student> defaultStudents = new List<Student>();
            IList<Teacher> defaultTeachers = new List<Teacher>();

            defaultStudents.Add(new Student()
            {
                FirstName = "TestFirstName1",
                LastName = "TestLastName1",
                Address = "11 Collins Street, Melbourne",
                DateOfBirth = new DateTime(1991, 1, 1),
                Gender = Gender.Male,
                Email = "Test1@yahoo.com",
                Id = 1,
                CountryId = 1
            });
            defaultStudents.Add(new Student()
            {
                FirstName = "TestFirstName2",
                LastName = "TestLastName2",
                Address = "22 Collins Street, Melbourne",
                DateOfBirth = new DateTime(1992, 2, 2),
                Gender = Gender.Female,
                Email = "Test2@yahoo.com",
                Id = 2,
                CountryId = 1
            });
            defaultStudents.Add(new Student()
            {
                FirstName = "TestFirstName3",
                LastName = "TestLastName3",
                Address = "33 Collins Street, Melbourne",
                DateOfBirth = new DateTime(1993, 3, 3),
                Gender = Gender.Other,
                Email = "Test3@yahoo.com",
                Id = 3,
                CountryId = 1
            });

            // Fix: Add Teacher objects to defaultTeachers instead of Student objects
            defaultTeachers.Add(new Teacher()
            {
                FirstName = "TestFirstName1",
                LastName = "TestLastName1",
                Address = "11 Collins Street, Melbourne",
                DateOfBirth = new DateTime(1991, 1, 1),
                Gender = Gender.Male,
                Email = "Test1@yahoo.com",
                Id = 1
            });
            defaultTeachers.Add(new Teacher()
            {
                FirstName = "TestFirstName2",
                LastName = "TestLastName2",
                Address = "22 Collins Street, Melbourne",
                DateOfBirth = new DateTime(1992, 2, 2),
                Gender = Gender.Female,
                Email = "Test2@yahoo.com",
                Id = 2
            });
            defaultTeachers.Add(new Teacher()
            {
                FirstName = "TestFirstName3",
                LastName = "TestLastName3",
                Address = "33 Collins Street, Melbourne",
                DateOfBirth = new DateTime(1993, 3, 3),
                Gender = Gender.Other,
                Email = "Test3@yahoo.com",
                Id = 3
            });
            

            modelBuilder.Entity<Student>().HasData(defaultStudents);
            modelBuilder.Entity<Teacher>().HasData(defaultTeachers);
        }
    }
}
