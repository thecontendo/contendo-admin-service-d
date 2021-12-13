using ContendoAdmin.Db.Extensions.DbContext;
using ContendoAdmin.Extensions.ApiExtensions;
using ContendoAdmin.Extensions.AppConfigExtensions;
using ContendoAdmin.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;



// Add services to the container.
var services = builder.Services;
services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
); ;

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddMemoryCache();
services.RegisterDbServices(configuration);
services.AddHttpContextAccessor();
services.AddSingleton<ITokenCacheService, TokenCacheService>();
services.AddEntityConfiguration();
services.AddAuthenticationConfiguration(configuration);
services.AddAuthorizationConfiguration();
services.AddSwaggerConfiguration();



//App Configurations
var app = builder.Build();
var env = app.Environment;
app.AddConfiguration(env);
app.MapControllers();

app.Run();


