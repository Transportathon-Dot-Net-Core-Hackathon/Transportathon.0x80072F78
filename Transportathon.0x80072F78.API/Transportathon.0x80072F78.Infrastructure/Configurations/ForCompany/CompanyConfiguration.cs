using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportathon._0x80072F78.Core.Entities.ForCompany;

namespace Transportathon._0x80072F78.Infrastructure.Configurations.Company;

public class CompanyConfiguration : IEntityTypeConfiguration<Core.Entities.ForCompany.Company>
{
    public void Configure(EntityTypeBuilder<Core.Entities.ForCompany.Company> builder)
    {
        builder.ToTable("Companies");
        builder.Property(x => x.CompanyName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(40);
        builder.Property(x => x.Surname).IsRequired().HasMaxLength(40);
        builder.Property(x => x.Address).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Street).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Alley).IsRequired().HasMaxLength(100);
        builder.Property(x => x.District).IsRequired().HasMaxLength(100);
        builder.Property(x => x.BuildingNumber).IsRequired().HasMaxLength(100);
        builder.Property(x => x.ApartmentNumber).IsRequired().HasMaxLength(100);
        builder.Property(x => x.PostCode).IsRequired().HasMaxLength(100);
        builder.Property(x => x.VKN).IsRequired().HasMaxLength(100);
        builder.Property(x => x.CompanyUsersId).IsRequired();

        builder.HasOne(c => c.CompanyUsers).WithMany().HasForeignKey(c=>c.CompanyUsersId);
    }
}