using Infrastructure.AuthService;
using Infrastructure.UnitOfWork;

namespace Api.Extensions;

public static class ApplicationServiceExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
