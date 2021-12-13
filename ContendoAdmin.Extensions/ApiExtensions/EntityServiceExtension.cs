using ContendoAdmin.Services.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ContendoAdmin.Extensions.ApiExtensions;

public static class EntityServiceExtension
{
    public static void AddEntityConfiguration(this IServiceCollection services)
    {
        services.AddTransient<IUserService, UserService>();
    }
}