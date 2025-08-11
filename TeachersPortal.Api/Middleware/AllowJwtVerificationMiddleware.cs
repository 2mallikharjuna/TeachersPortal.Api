using TeachersPortal.Api.Application.Interfaces.Auth;

namespace TeachersPortal.Api.Middleware
{
    public class AllowJwtVerificationMiddleware
    {
        private readonly RequestDelegate _next;
        public AllowJwtVerificationMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext ctx, ITokenService ts)
        {
            var auth = ctx.Request.Headers["Authorization"].FirstOrDefault();
            if (!string.IsNullOrEmpty(auth) && auth.StartsWith("Bearer "))
            {
                var token = auth.Substring("Bearer ".Length).Trim();
                try
                {
                    ctx.User = ts.ValidateToken(token);
                }
                catch
                {
                    ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await ctx.Response.WriteAsync("Invalid token"); return;
                }
            }
            await _next(ctx);
        }
    }
}
