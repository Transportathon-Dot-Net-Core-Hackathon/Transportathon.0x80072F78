using Transportathon._0x80072F78.Core.DTOs.Identity;
using Transportathon._0x80072F78.Shared.Models;

namespace Transportathon._0x80072F78.Services.Identity;

public interface IUserService
{
    Task<CustomResponse<UserDTO>> GetUserAsync();
    Task<CustomResponse<UserDTO>> CreateUserAsync(CreateUserDTO createUserDTO);
}