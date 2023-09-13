using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Transportathon._0x80072F78.Core.DTOs.Identity;
using Transportathon._0x80072F78.Services.Identity;
using Transportathon._0x80072F78.Shared.Models;

namespace Transportathon._0x80072F78.API.Controllers.Identity;

public class AuthController : CustomBaseController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<CustomResponse<TokenDTO>>> Login(LoginDTO loginDTO)
    {
        return CreateActionResultInstance(await _authService.CreateTokenAsync(loginDTO));
    }

    [HttpPost]
    [AllowAnonymous]
    public ActionResult<CustomResponse<ClientTokenDTO>> CreateTokenByClient(ClientLoginDTO clientLoginDTO)
    {
        return CreateActionResultInstance(_authService.CreateTokenByClient(clientLoginDTO));
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<CustomResponse<TokenDTO>>> CreateTokenByRefreshToken(RefreshTokenDTO refreshTokenDto)
    {
        return CreateActionResultInstance(await _authService.CreateTokenByRefreshTokenAsync(refreshTokenDto.Token));
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<CustomResponse<TokenDTO>>> RevokeRefreshToken(RefreshTokenDTO refreshTokenDto)
    {
        return CreateActionResultInstance(await _authService.RevokeRefreshTokenAsync(refreshTokenDto.Token));
    }
}