namespace TeachersPortal.Api.Application.Requests
{
    public class SignupRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int TeacherId { get; set; } // Link user to teacher
    }
}
