using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportathon._0x80072F78.Core.Entities.Company;

namespace Transportathon._0x80072F78.Infrastructure.Configurations.Company;

public class DriverConfiguration : IEntityTypeConfiguration<Core.Entities.Company.Driver>
{
    public void Configure(EntityTypeBuilder<Driver> builder)
    {
        builder.ToTable("Drivers");
        builder.Property(x => x.Name).IsRequired().HasMaxLength(40);
        builder.Property(x => x.Surname).IsRequired().HasMaxLength(40);
        builder.Property(x => x.Experience).IsRequired().HasMaxLength(40);
        builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(15);
        builder.Property(x => x.EMail).IsRequired().HasMaxLength(40);
        builder.Property(x => x.Age).IsRequired().HasMaxLength(20);
        builder.Property(x => x.DrivingLicenceTypes).IsRequired();
    }
}