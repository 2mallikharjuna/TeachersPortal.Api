using System;
using TeachPortal.Domain.Entities;

namespace TeachPortal.Application.DTOs.Response
{
    public record TeacherDto(int Id, string FirstName, string LastName, string Email)
    {
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
    }
}
