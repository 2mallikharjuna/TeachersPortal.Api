using TeachPortal.Api.DatContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeachersPortal.Api.Domain.Entities;
using TeachersPortal.Application.Services;
using TeachersPortal.Application.Interfaces;
using TeachersPortal.Api.Domain.Repositories;
using TeachersPortal.Api.Domain.Repositories.Core;
using TeachersPortal.Api.Application.Services.Auth;
using TeachersPortal.Api.Application.Interfaces.Auth;
using TeachPortal.Infrastructure.Data.Core;
using TeachPortal.Infrastructure.Data.Repositories;
using TeachersPortal.Api.Middleware;

namespace TeachersPortal.Api.App_Start
{
    /// <summary>
    /// Dependency injector of all services
    /// </summary>
    public static class DependencyInjector
    {
        /// <summary>
        /// Extension method to add the dependencies
        /// </summary>
        /// <param name="services">Base services</param>
        /// <param name="configuration">Base configuration</param>
        /// <returns></returns>
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<IConfiguration>(configuration);
            services.AddSingleton(configuration);
            services.InjectDependencies(configuration);
            services.AddHealthChecks();
            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
            }));

            return services;
        }

        /// <summary>
        /// Create a dependency injection
        /// </summary>
        /// <param name="services">Base services</param>
        /// <param name="configuration"></param>
        public static void InjectDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // Repositories            
            services.AddScoped(typeof(IRepository<,>), typeof(BaseRepository<,>));

            // Identity utils
            services.AddScoped<IPasswordHasher<Teacher>, PasswordHasher<Teacher>>();

            // Services
            services.AddScoped<ITokenService, JwtTokenService>();
            services.AddScoped<ITeachersAppService, TeachersAppService>();
            services.AddScoped<IStudentsAppService, StudentsAppService>();
            services.AddScoped<ICoursesAppService, CoursesAppService>();
                       
            services.AddScoped<TpApiDbContext>(provider =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<TpApiDbContext>();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("TpApiContext"));
                return new TpApiDbContext(optionsBuilder.Options);
            });

            // Register repositories with required dependencies
            services.AddScoped<ITeacherRepository, TeacherRepository>(provider =>
            {
                var dbContextFactory = provider.GetRequiredService<IDbContextFactory<TpApiDbContext>>();
                var dbContext = dbContextFactory.CreateDbContext();
                return new TeacherRepository(dbContext);
            });

            services.AddScoped<IStudentRepository, StudentRepository>(provider =>
            {
                var dbContextFactory = provider.GetRequiredService<IDbContextFactory<TpApiDbContext>>();
                var dbContext = dbContextFactory.CreateDbContext();
                return new StudentRepository(dbContext);
            });

            services.AddScoped<ICoursesRepository, CoursesRepository>(provider =>
            {
                var dbContextFactory = provider.GetRequiredService<IDbContextFactory<TpApiDbContext>>();
                var dbContext = dbContextFactory.CreateDbContext();
                return new CoursesRepository(dbContext);
            });
        }

        public static IApplicationBuilder AppApiMiddlewares(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<RequestLoggingMiddleware>();
            builder.UseMiddleware<AllowJwtVerificationMiddleware>();
            return builder;
        }
    }
}
