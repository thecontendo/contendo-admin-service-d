using ContendoAdmin.Db.Context;
using ContendoAdmin.Models;
using ContendoAdmin.Models.Dto;
using ContendoAdmin.Models.Identity;
using Microsoft.Extensions.Configuration;

namespace ContendoAdmin.Services.Identity;

public class RoleService: IRoleService
{
    private readonly AppDbContext _db;
    private readonly ITokenCacheService _tokenCacheService;
    private readonly IConfiguration _configuration;

    public RoleService(AppDbContext dbContext, ITokenCacheService tokenCacheService, IConfiguration configuration)
    {
        _db = dbContext;
        _tokenCacheService = tokenCacheService;
        _configuration = configuration;
    }
    
    public async Task<ApiResponse<List<Role>>> Get()
    {
        
        throw new NotImplementedException();
    }

    public async Task<ApiResponse<bool>> Post(RoleDto data)
    {
        var response = new ApiResponse<bool>();
        try
        {
            var result = await _db.Roles.AddAsync(new Role
            {
                Title = data.Title
            });
            await _db.SaveChangesAsync();

            response.Data = true;
            response.AddSuccess();
            return await Task.FromResult(response);
        }
        catch (Exception ex)
        {
            response.AddError(ex);
            return await Task.FromResult(response);
        }
        throw new NotImplementedException();
    }
}