using TeachPortal.Api.DatContext;
using TeachersPortal.Api.App_Start;
using Microsoft.EntityFrameworkCore;
using TeachersPortal.Api.Middleware;

namespace TeachersPortal.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Caching    
            builder.Services.AddMemoryCache();

            // JWT Auth for ASP.NET Core    
            builder.Services.AddJwtAuthentication(builder.Configuration);

            // Pass the required 'configuration' parameter  
            builder.Services.AddDependencies(builder.Configuration);

            builder.Services.AddAuthorization();

            
            builder.Services.AddDbContextFactory<TpApiDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("TpApiContext")));

            var app = builder.Build();
            app.AppApiMiddlewares();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
