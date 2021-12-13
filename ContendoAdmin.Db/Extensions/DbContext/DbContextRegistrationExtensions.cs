using ContendoAdmin.Db.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContendoAdmin.Db.Extensions.DbContext;

public static class DbContextRegistrationExtensions
{
    public static void RegisterDbServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => {
            options.UseNpgsql(configuration.GetConnectionString("main"));
        });
    }
}