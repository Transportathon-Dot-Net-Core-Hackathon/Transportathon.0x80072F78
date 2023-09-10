namespace Transportathon._0x80072F78.Shared.Interfaces;

public interface IHttpContextData
{
    string? UserId { get; }
    string? UserName { get; }
    string? Token { get; }
    List<string> UserRoleNames { get; }
    string RequestUrl { get; }
}