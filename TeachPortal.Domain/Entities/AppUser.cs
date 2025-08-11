using System;
using TeachersPortal.Api.Domain.Entities;

namespace TeachPortal.Domain.Entities
{
    public class AppUser : BaseEntity
    {
        public int TeacherId { get; set; }
        public string Username { get; set; }
        public string HashPassword { get; set; } // Store hashed, never plaintext
        public Teacher Teacher { get; set; }
    }
}
