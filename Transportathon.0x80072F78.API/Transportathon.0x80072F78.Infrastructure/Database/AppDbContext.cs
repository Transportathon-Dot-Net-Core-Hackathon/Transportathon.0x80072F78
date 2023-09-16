using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Transportathon._0x80072F78.Core.Entities;
using Transportathon._0x80072F78.Core.Entities.ForCompany;
using Transportathon._0x80072F78.Core.Entities.Identity;
using Transportathon._0x80072F78.Core.Entities.Offer;

namespace Transportathon._0x80072F78.Infrastructure.Database;

public class AppDbContext : IdentityDbContext<AspNetUser, AspNetRole, Guid,
AspNetUserClaim, AspNetUserRole, AspNetUserLogin, AspNetRoleClaim, AspNetUserToken>, IDataProtectionKeyContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
    public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<TeamWorker> TeamWorkers { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<TransportationRequest> TransportationRequests { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<MyMessage> MyMessages { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        base.OnModelCreating(builder);
    }
}
