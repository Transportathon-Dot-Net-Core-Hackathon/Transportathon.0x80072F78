using Transportathon._0x80072F78.Core.DTOs.Identity;
using Transportathon._0x80072F78.Shared.Models;

namespace Transportathon._0x80072F78.Services.Identity;

public interface IAuthService
{
    Task<CustomResponse<TokenDTO>> CreateTokenAsync(LoginDTO loginDTO);
    Task<CustomResponse<TokenDTO>> CreateTokenByRefreshTokenAsync(string refreshToken);
    Task<CustomResponse<NoContent>> RevokeRefreshTokenAsync(string refreshToken);
    CustomResponse<ClientTokenDTO> CreateTokenByClient(ClientLoginDTO clientLoginDto);
}