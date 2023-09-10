namespace Transportathon._0x80072F78.Shared.Options;

public class CustomTokenOptions
{
    public List<string> Audience { get; set; }
    public string Issuer { get; set; }
    public int AccessTokenExpiration { get; set; }
    public int RefreshTokenExpiration { get; set; }
    public string SecurityKey { get; set; }
    public string PrivateKey { get; set; }
    public string PublicKey { get; set; }
}