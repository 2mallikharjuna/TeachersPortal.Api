using Microsoft.AspNetCore.Mvc;
using TeachPortal.Application.Interfaces;
using TeachersPortal.Api.Application.Requests;

namespace TeachersPortal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authAppService;

        public AuthController(IAuthService authService) => _authAppService = authService;

        /// <summary>
        /// Registers a new teacher account.
        /// </summary>
        /// <param name="req">Signup request containing username, password, and teacher ID.</param>
        /// <returns>Returns 200 OK if successful, 400 Bad Request for argument errors, or 409 Conflict for invalid operations.</returns>
        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupRequest req)
        {
            try
            {
                await _authAppService.SignupAsync(req);
                return Ok("Signup successful");
            }
            catch (ArgumentException ae) { return BadRequest(ae.Message); }
            catch (InvalidOperationException ioe) { return Conflict(ioe.Message); }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            var res = await _authAppService.LoginTeacherAsync(req);
            if (res == null) return Unauthorized("Invalid credentials");
            return Ok(res);
        }
    }   
}

