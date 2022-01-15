using ICBF.Application.Interfaces;
using ICBF.Application.Services;
using ICBF.Domain.Helper;
using ICBF.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ICBF.Api.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
