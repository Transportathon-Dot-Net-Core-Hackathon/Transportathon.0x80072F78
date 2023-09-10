using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Transportathon._0x80072F78.Core.DTOs.Identity;
using Transportathon._0x80072F78.Core.Entities.Identity;
using Transportathon._0x80072F78.Core.Interfaces;
using Transportathon._0x80072F78.Core.Options;
using Transportathon._0x80072F78.Shared.Options;

namespace Transportathon._0x80072F78.Services.Identity;

public class TokenService : ITokenService
{
    private readonly UserManager<AspNetUser> _userManager;
    private readonly CustomTokenOptions _customTokenOptions;
    public TokenService(UserManager<AspNetUser> userManager, IOptions<CustomTokenOptions> options)
    {
        _userManager = userManager;
        _customTokenOptions = options.Value;
    }

    private string CreateRefreshToken()
    {
        var numberByte = new byte[32];
        using var randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(numberByte);

        return Convert.ToBase64String(numberByte);
    }

    private async Task<List<Claim>> GetClaims(AspNetUser aspNetUser, List<string> audiences)
    {
        var listOfClaims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier, aspNetUser.Id.ToString()),
            new(ClaimTypes.Name, aspNetUser.UserName),
            new(JwtRegisteredClaimNames.UniqueName, aspNetUser.UserName)
        };

        listOfClaims.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));

        var roles = await _userManager.GetRolesAsync(aspNetUser);
        listOfClaims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return listOfClaims;
    }

    private static IEnumerable<Claim> GetClaimsByClient(Client client)
    {
        var listOfClaims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Sub, client.Id)
        };

        listOfClaims.AddRange(client.Audience.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
        return listOfClaims;
    }

    public async Task<TokenDTO> CreateToken(AspNetUser aspNetUser)
    {
        var accessTokenExpiration = DateTime.UtcNow.AddMinutes(_customTokenOptions.AccessTokenExpiration);
        var refreshTokenExpiration = DateTime.UtcNow.AddMinutes(_customTokenOptions.RefreshTokenExpiration);

        var signingCredential = RsaServices.GetSigningCredentialsWithPrivateKey(_customTokenOptions.PrivateKey);
        var tokenClaims = await GetClaims(aspNetUser, _customTokenOptions.Audience);

        JwtSecurityToken jwtSecurityToken = new(
            _customTokenOptions.Issuer,
            expires: accessTokenExpiration,
            notBefore: DateTime.UtcNow,
            claims: tokenClaims,
            signingCredentials: signingCredential
            );

        var handler = new JwtSecurityTokenHandler();
        var tokenString = handler.WriteToken(jwtSecurityToken);

        var tokenDto = new TokenDTO
        {
            Token = tokenString,
            RefreshToken = CreateRefreshToken(),
            RefreshTokenExpiration = refreshTokenExpiration
        };

        return tokenDto;
    }

    public ClientTokenDTO CreateTokenByClient(Client client)
    {
        var accessTokenExpiration = DateTime.UtcNow.AddMinutes(_customTokenOptions.AccessTokenExpiration);
        var signingCredential = RsaServices.GetSigningCredentialsWithPrivateKey(_customTokenOptions.PrivateKey);

        JwtSecurityToken jwtSecurityToken = new(
            _customTokenOptions.Issuer,
            expires: accessTokenExpiration,
            notBefore: DateTime.UtcNow,
            //claims: GetClaimsByClient(client),
            signingCredentials: signingCredential);

        var handler = new JwtSecurityTokenHandler();
        var token = handler.WriteToken(jwtSecurityToken);

        var tokenDto = new ClientTokenDTO
        {
            AccessToken = token,
            AccessTokenExpiration = accessTokenExpiration
        };

        return tokenDto;
    }
}