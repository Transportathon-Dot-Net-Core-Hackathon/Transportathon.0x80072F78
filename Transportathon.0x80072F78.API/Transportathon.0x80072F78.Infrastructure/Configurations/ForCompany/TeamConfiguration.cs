using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportathon._0x80072F78.Core.Entities.ForCompany;

namespace Transportathon._0x80072F78.Infrastructure.Configurations.Company;

public class TeamConfiguration : IEntityTypeConfiguration<Core.Entities.ForCompany.Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.ToTable("Teams");
        builder.Property(x => x.CompanyId).IsRequired();

        builder.HasOne(c => c.Company).WithMany().HasForeignKey(c => c.CompanyId);
    }
}