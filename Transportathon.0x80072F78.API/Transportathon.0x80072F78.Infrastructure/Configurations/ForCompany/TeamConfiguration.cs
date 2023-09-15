using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transportathon._0x80072F78.Core.Entities.ForCompany;

namespace Transportathon._0x80072F78.Infrastructure.Configurations.Company;

public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.ToTable("Teams");
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.CompanyId).IsRequired();
        builder.Property(x => x.UserId).IsRequired();

        builder.HasIndex(x => new { x.Name, x.CompanyId })
            .HasDatabaseName("uCompanyIdName").IsUnique();

        builder.HasOne(c => c.Company).WithMany().HasForeignKey(c => c.CompanyId).OnDelete(DeleteBehavior.NoAction);
    }
}