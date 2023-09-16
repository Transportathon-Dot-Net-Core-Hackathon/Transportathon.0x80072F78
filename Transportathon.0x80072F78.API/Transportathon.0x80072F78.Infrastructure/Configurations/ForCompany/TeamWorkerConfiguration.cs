using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportathon._0x80072F78.Core.Entities.ForCompany;

namespace Transportathon._0x80072F78.Infrastructure.Configurations.Company;

public class TeamWorkerConfiguration : IEntityTypeConfiguration<Core.Entities.ForCompany.TeamWorker>
{
    public void Configure(EntityTypeBuilder<TeamWorker> builder)
    {
        builder.ToTable("TeamWorkers");
        builder.Property(x => x.Name).IsRequired().HasMaxLength(40);
        builder.Property(x => x.Surname).IsRequired().HasMaxLength(40);
        builder.Property(x => x.Age).IsRequired().HasMaxLength(5);
        builder.Property(x => x.PhoneNumber).HasMaxLength(15);
        builder.Property(x => x.EMail).HasMaxLength(50);
        builder.Property(x => x.Experience).HasMaxLength(10);
    }
}