using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Transportathon._0x80072F78.Core.DTOs.Identity;
using Transportathon._0x80072F78.Core.Entities.Identity;
using Transportathon._0x80072F78.Shared.Interfaces;
using Transportathon._0x80072F78.Shared.Models;

namespace Transportathon._0x80072F78.Services.Identity;

public class UserService : IUserService
{
    private readonly UserManager<AspNetUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IHttpContextData _httpContextData;

    public UserService(UserManager<AspNetUser> userManager, IMapper mapper, IHttpContextData httpContextData)
    {
        _userManager = userManager;
        _mapper = mapper;
        _httpContextData = httpContextData;
    }

    public async Task<CustomResponse<UserDTO>> CreateUserAsync(CreateUserDTO createUserDTO)
    {
        AspNetUser aspNetUser = _mapper.Map<AspNetUser>(createUserDTO);

        var result = await _userManager.CreateAsync(aspNetUser, createUserDTO.Password);
        if (!result.Succeeded)
            return CustomResponse<UserDTO>.Fail(StatusCodes.Status500InternalServerError,
                result.Errors.Select(k => k.Description).ToList());

        UserDTO userDTO = _mapper.Map<UserDTO>(aspNetUser);

        return CustomResponse<UserDTO>.Success(StatusCodes.Status200OK, userDTO);
    }

    public async Task<CustomResponse<UserDTO>> GetUserAsync()
    {
        AspNetUser user = await _userManager.FindByIdAsync(_httpContextData.UserId);
        if (user == null)
            return CustomResponse<UserDTO>.Fail(StatusCodes.Status404NotFound, nameof(GetUserAsync));

        UserDTO userDTO = _mapper.Map<UserDTO>(user);

        return CustomResponse<UserDTO>.Success(StatusCodes.Status200OK, userDTO);
    }
}