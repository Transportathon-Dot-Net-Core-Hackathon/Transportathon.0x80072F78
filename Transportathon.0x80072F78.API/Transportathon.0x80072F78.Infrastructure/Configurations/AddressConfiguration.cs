using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportathon._0x80072F78.Core.Entities;

namespace Transportathon._0x80072F78.Infrastructure.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("Addresses");
        builder.Property(x => x.AddressType).IsRequired().HasMaxLength(150);
        builder.Property(x => x.AddressName).IsRequired().HasMaxLength(20);
        builder.Property(x => x.City).IsRequired().HasMaxLength(20);
        builder.Property(x => x.District).IsRequired().HasMaxLength(20);
        builder.Property(x => x.Street).IsRequired().HasMaxLength(20);
        builder.Property(x => x.Alley).IsRequired().HasMaxLength(20);
        builder.Property(x => x.BuildingNumber).IsRequired().HasMaxLength(5);
        builder.Property(x => x.ApartmentNumber).IsRequired().HasMaxLength(5);
        builder.Property(x => x.PostCode).IsRequired().HasMaxLength(6);
    }
}