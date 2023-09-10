using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Transportathon._0x80072F78.Core.Entities.Identity;

namespace Transportathon._0x80072F78.Infrastructure.Configurations.Identity;

public class AspNetUserConfiguration : IEntityTypeConfiguration<AspNetUser>
{
    public void Configure(EntityTypeBuilder<AspNetUser> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.UserName).IsRequired().HasMaxLength(128);
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.FamilyName).IsRequired().HasMaxLength(100);
    }
}