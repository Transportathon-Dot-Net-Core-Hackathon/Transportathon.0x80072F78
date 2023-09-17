using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transportathon._0x80072F78.Core.Entities.ForCompany;
namespace Transportathon._0x80072F78.Infrastructure.Configurations.Company;

public class DriverConfiguration : IEntityTypeConfiguration<Driver>
{
    public void Configure(EntityTypeBuilder<Driver> builder)
    {
        builder.ToTable("Drivers");
        builder.Property(x => x.Name).IsRequired().HasMaxLength(40);
        builder.Property(x => x.Surname).IsRequired().HasMaxLength(40);
        builder.Property(x => x.Experience).HasMaxLength(40);
        builder.Property(x => x.PhoneNumber).HasMaxLength(15);
        builder.Property(x => x.EMail).HasMaxLength(40);
        builder.Property(x => x.Age).IsRequired();
        builder.Property(x => x.DrivingLicenceTypes).IsRequired();
        builder.Property(x => x.UserId).IsRequired();
    }
}