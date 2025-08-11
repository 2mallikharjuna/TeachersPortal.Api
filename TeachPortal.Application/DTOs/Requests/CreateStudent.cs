using System;


namespace TeachPortal.Application.DTOs.Requests
{
    public class CreateStudent
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
