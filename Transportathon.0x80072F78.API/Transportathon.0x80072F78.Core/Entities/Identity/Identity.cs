using Microsoft.AspNetCore.Identity;

namespace Transportathon._0x80072F78.Core.Entities.Identity;

public class AspNetUser : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string FamilyName { get; set; }
    public string? UserImage { get; set; }
}

public class AspNetRole : IdentityRole<Guid>
{
    public string Description { get; set; }
}

public class AspNetUserRole : IdentityUserRole<Guid>
{
}

public class AspNetUserClaim : IdentityUserClaim<Guid>
{
}

public class AspNetUserLogin : IdentityUserLogin<Guid>
{
}

public class AspNetUserToken : IdentityUserToken<Guid>
{
}

public class AspNetRoleClaim : IdentityRoleClaim<Guid>
{
}