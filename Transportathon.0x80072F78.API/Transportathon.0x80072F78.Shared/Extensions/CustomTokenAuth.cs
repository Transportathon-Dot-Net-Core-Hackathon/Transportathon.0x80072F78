using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using Transportathon._0x80072F78.Shared.Constants;

namespace Transportathon._0x80072F78.Shared.Extensions;

public static class CustomTokenAuth
{
    public static void AddCustomTokenAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var privateKey = LoadPrivateKey(configuration);
        var publicKey = LoadPublicKey(configuration);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opts =>
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new RsaSecurityKey(publicKey),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero

            };
            opts.TokenValidationParameters = tokenValidationParameters;

        });
    }
    private static RSA LoadPrivateKey(IConfiguration configuration)
    {
        string privateKeyString = configuration.GetSection(ConfigurationSectionConst.TokenOptions)[ConfigurationEntityConst.PrivateKey];
        byte[] privateKeyBytes = Convert.FromBase64String(privateKeyString);

        var rsa = RSA.Create();
        rsa.ImportRSAPrivateKey(privateKeyBytes, out _);
        return rsa;
    }

    private static RSA LoadPublicKey(IConfiguration configuration)
    {
        string publicKeyString = configuration.GetSection(ConfigurationSectionConst.TokenOptions)[ConfigurationEntityConst.PublicKey];
        byte[] publicKeyBytes = Convert.FromBase64String(publicKeyString);

        var rsa = RSA.Create();
        rsa.ImportRSAPublicKey(publicKeyBytes, out _);
        return rsa;
    }
}