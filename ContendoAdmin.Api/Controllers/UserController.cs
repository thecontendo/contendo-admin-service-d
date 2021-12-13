using ContendoAdmin.Models.Dto;
using ContendoAdmin.Services.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContendoAdmin.Api.Controllers;

public class UserController: BaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        this._userService = userService;
    }
    
    /// <summary>
    /// Get list of users\
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _userService.Get());
    }
    
    /// <summary>
    /// Create user 
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post(UserCreateDto data)
    {
        return Ok(await _userService.Post(data));
    }
    
    /// <summary>
    /// Get Microsoft GraphUsers
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet("GetGraphUsers/{param}")]
    public async Task<IActionResult> GetGraphUsers(string param)
    {
        return Ok(await _userService.GetGraphUsers(param));
    }
    
    /// <summary>
    /// Send User Invitation
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet("SendUserInvitation")]
    public async Task<IActionResult> SendUserInvitation()
    {
        return Ok(await _userService.SendUserInvitation());
    }
    
    /// <summary>
    /// Send User Invitation
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet("CreateUser")]
    public async Task<IActionResult> CreateUser()
    {
        return Ok(await _userService.CreateUser());
    }
}