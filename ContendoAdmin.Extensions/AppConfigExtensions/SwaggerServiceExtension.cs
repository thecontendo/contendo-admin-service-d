using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ContendoAdmin.Extensions.AppConfigExtensions;

public static class SwaggerServiceExtension
{
    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Contendo Admin",
                Description = "Contendo Admin Api - Dot Net Application",
                Version = "v1",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "TheContendo Gmbh",
                    Url = new Uri("https://example.com/contact")
                },
                License = new OpenApiLicense
                {
                    Name = "MIT",
                    Url = new Uri("https://example.com/license")
                }
            });
        });
    }
}