namespace Transportathon._0x80072F78.Core.Entities.Identity;

public class UserRefreshToken
{
    public Guid UserId { get; set; }
    public string Code { get; set; }
    public DateTime Expiration { get; set; }
}