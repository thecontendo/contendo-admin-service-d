using ContendoAdmin.Models;
using ContendoAdmin.Models.Dto;
using ContendoAdmin.Models.Identity;

namespace ContendoAdmin.Services.Identity;

public interface IRoleService
{
    Task<ApiResponse<List<Role>>> Get();

    Task<ApiResponse<bool>> Post(RoleDto data);
}