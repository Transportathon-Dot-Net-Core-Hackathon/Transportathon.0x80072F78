using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Transportathon._0x80072F78.Core.DTOs.Identity;
using Transportathon._0x80072F78.Core.Entities.Identity;
using Transportathon._0x80072F78.Core.Interfaces;
using Transportathon._0x80072F78.Core.Options;
using Transportathon._0x80072F78.Core.Repository;
using Transportathon._0x80072F78.Shared.Models;

namespace Transportathon._0x80072F78.Services.Identity;

public class AuthService : IAuthService
{
    private readonly List<Client> _clients;
    private readonly ITokenService _tokenService;
    private readonly UserManager<AspNetUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly SignInManager<AspNetUser> _signInManager;

    public AuthService(IOptions<List<Client>> clients, ITokenService tokenService, UserManager<AspNetUser> userManager, IUnitOfWork unitOfWork, SignInManager<AspNetUser> signInManager)
    {
        _clients = clients.Value;
        _tokenService = tokenService;
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _signInManager = signInManager;
    }

    public async Task<CustomResponse<TokenDTO>> CreateTokenAsync(LoginDTO loginDTO)
    {
        AspNetUser user = await CheckUserPasswordAsync(loginDTO.UserName, loginDTO.Password);
        if (user == null)
            return CustomResponse<TokenDTO>.Fail(StatusCodes.Status400BadRequest, nameof(CreateTokenAsync));

        var token = await _tokenService.CreateToken(user);

        UserRefreshToken userRefreshToken = await _unitOfWork.UserRefreshTokenRepository.GetByIdAsync(user.Id);
        if (userRefreshToken == null)
            await _unitOfWork.UserRefreshTokenRepository.CreateAsync(new UserRefreshToken { UserId = user.Id, Code = token.RefreshToken, Expiration = token.RefreshTokenExpiration });
        else
        {
            userRefreshToken.Code = token.RefreshToken;
            userRefreshToken.Expiration = token.RefreshTokenExpiration;
        }

        await _unitOfWork.SaveAsync();

        return CustomResponse<TokenDTO>.Success(StatusCodes.Status200OK, token);
    }

    public CustomResponse<ClientTokenDTO> CreateTokenByClient(ClientLoginDTO clientLoginDTO)
    {
        Client client = _clients.SingleOrDefault(x => x.Id == clientLoginDTO.ClientId && x.Secret == clientLoginDTO.ClientSecret);
        if (client == null)
            return CustomResponse<ClientTokenDTO>.Fail(StatusCodes.Status404NotFound, nameof(CreateTokenByClient));

        ClientTokenDTO token = _tokenService.CreateTokenByClient(client);

        return CustomResponse<ClientTokenDTO>.Success(StatusCodes.Status200OK, token);
    }

    public async Task<CustomResponse<TokenDTO>> CreateTokenByRefreshTokenAsync(string refreshToken)
    {
        UserRefreshToken refreshTokenEntity = await _unitOfWork.UserRefreshTokenRepository.SingleOrDefaultAsync(x => x.Code == refreshToken);
        if (refreshTokenEntity == null)
            return CustomResponse<TokenDTO>.Fail(StatusCodes.Status404NotFound, nameof(CreateTokenByRefreshTokenAsync));

        if (!RefreshTokenIsValid(refreshTokenEntity))
            return CustomResponse<TokenDTO>.Fail(StatusCodes.Status400BadRequest, nameof(CreateTokenByRefreshTokenAsync));

        AspNetUser user = await _userManager.FindByIdAsync(refreshTokenEntity.UserId.ToString());
        if (user == null)
            return CustomResponse<TokenDTO>.Fail(StatusCodes.Status404NotFound, nameof(CreateTokenByRefreshTokenAsync));

        TokenDTO tokenDto = await _tokenService.CreateToken(user);
        refreshTokenEntity.Code = tokenDto.RefreshToken;
        refreshTokenEntity.Expiration = tokenDto.RefreshTokenExpiration;

        await _unitOfWork.SaveAsync();

        return CustomResponse<TokenDTO>.Success(StatusCodes.Status200OK, tokenDto);
    }

    public async Task<CustomResponse<NoContent>> RevokeRefreshTokenAsync(string refreshToken)
    {
        UserRefreshToken refreshTokenEntity = await _unitOfWork.UserRefreshTokenRepository.SingleOrDefaultAsync(x => x.Code == refreshToken);
        if (refreshTokenEntity == null)
            return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(RevokeRefreshTokenAsync));

        await _unitOfWork.UserRefreshTokenRepository.DeleteAsync(refreshTokenEntity);
        await _unitOfWork.SaveAsync();

        return CustomResponse<NoContent>.Success(StatusCodes.Status200OK);
    }

    private async Task<AspNetUser> CheckUserPasswordAsync(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null) return null;

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        if (!result.Succeeded) return null;

        return user;
    }

    private static bool RefreshTokenIsValid(UserRefreshToken userRefreshToken)
    {
        if (userRefreshToken.Expiration > DateTime.UtcNow)
            return true;
        else
            return false;
    }
}