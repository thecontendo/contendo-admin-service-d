using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace ContendoAdmin.Extensions.AppConfigExtensions;

public static class AuthorizationServiceExtension
    {
        public static void AddAuthorizationConfiguration(this IServiceCollection services)
        {
            /*services.AddAuthorization(options =>
            {
                options.AddPolicy("IsSuperAdmin", policy =>
                    policy.Requirements.Add(new SuperAdminPermission(PermissionEnum.Definitions)));
                options.AddPolicy("IsLandscapeAdmin", policy =>
                   policy.Requirements.Add(new LandscapeAdminPermission(PermissionEnum.DefineLandscapeAdmin)));
            });*/

            /*services.AddSingleton<IAuthorizationHandler, PermissionHandler>();*/

            services.AddCors(options =>
            {
                options.AddPolicy("APIPolicy", builder =>
                {
                    builder
                    .AllowAnyOrigin()
                    .WithOrigins("http://localhost:3001",
                    "http://localhost:3000",
                    "http://localhost:5000")
                    .AllowCredentials()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowedToAllowWildcardSubdomains();
                });

            });


        }
    }