using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Transportathon._0x80072F78.Core.DTOs.Identity;
using Transportathon._0x80072F78.Services.Identity;
using Transportathon._0x80072F78.Shared.Models;

namespace Transportathon._0x80072F78.API.Controllers.Identity;

public class UserController : CustomBaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<CustomResponse<UserDTO>>> GetUser()
    {
        return CreateActionResultInstance(await _userService.GetUserAsync());
    }

    [HttpPost]
    public async Task<ActionResult<CustomResponse<IdentityResult>>> CreateUser(CreateUserDTO createUserDTO)
    {
        return CreateActionResultInstance(await _userService.CreateUserAsync(createUserDTO));
    }
}