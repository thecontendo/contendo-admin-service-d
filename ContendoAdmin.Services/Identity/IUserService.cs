using ContendoAdmin.Models;
using ContendoAdmin.Models.Dto;
using ContendoAdmin.Models.Identity;

namespace ContendoAdmin.Services.Identity;

public interface IUserService
{
    Task<ApiResponse<List<UserDto>>> Get();

    Task<ApiResponse<User>> Post(UserCreateDto data);
    Task<ApiResponse<List<UserDto>>> GetGraphUsers(string param);
    
    Task<ApiResponse<bool>> SendUserInvitation();
    Task<ApiResponse<bool>> CreateUser();

}