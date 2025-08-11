using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachersPortal.Api.Application.Requests;

namespace TeachPortal.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginTeacherAsync(LoginRequest req); // returns JWT token
        Task SignupAsync(SignupRequest req);
    }
}
