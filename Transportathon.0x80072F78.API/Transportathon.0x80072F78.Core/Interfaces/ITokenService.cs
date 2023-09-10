using Transportathon._0x80072F78.Core.DTOs.Identity;
using Transportathon._0x80072F78.Core.Entities.Identity;
using Transportathon._0x80072F78.Core.Options;

namespace Transportathon._0x80072F78.Core.Interfaces;

public interface ITokenService
{
    Task<TokenDTO> CreateToken(AspNetUser aspNetUser);
    ClientTokenDTO CreateTokenByClient(Client client);
}