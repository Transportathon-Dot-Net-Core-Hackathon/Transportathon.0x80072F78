using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace Transportathon._0x80072F78.Services;

public static class RsaServices
{
    private static readonly RSA _rsa = RSA.Create();

    public static SigningCredentials GetSigningCredentialsWithPrivateKey(string privateKey)
    {
        _rsa.ImportRSAPrivateKey(
            Convert.FromBase64String(privateKey),
            out _);

        return new SigningCredentials(
            new RsaSecurityKey(_rsa),
            SecurityAlgorithms.RsaSha256 // Important to use RSA version of the SHA algo 
        );
    }

    public static SecurityKey GetSecurityKeyWithPublicKey(string publicKey)
    {
        _rsa.ImportRSAPublicKey(
            Convert.FromBase64String(publicKey),
            out _
        );
        return new RsaSecurityKey(_rsa);
    }
}