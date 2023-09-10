namespace Transportathon._0x80072F78.Core.DTOs.Identity;

public class TokenDTO
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiration { get; set; }
}