﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportathon._0x80072F78.Core.Entities.Company;

namespace Transportathon._0x80072F78.Infrastructure.Configurations.Company;

public class VehicleConfiguration : IEntityTypeConfiguration<Core.Entities.Company.Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
       builder.ToTable("Vehicles");
        builder.Property(x => x.VehicleType).IsRequired().HasMaxLength(100);
        builder.Property(x => x.VehicleLicensePlate).IsRequired().HasMaxLength(100);
        builder.Property(x => x.VehicleVolumeCapacity).IsRequired().HasMaxLength(40);
        builder.Property(x => x.VehicleWeightCapacity).IsRequired().HasMaxLength(40);
        builder.Property(x => x.VehicleStatus).IsRequired().HasMaxLength(150);
        builder.Property(x => x.DriverId).IsRequired().HasMaxLength(100);

        builder.HasOne(c => c.Driver).WithMany().HasForeignKey(c => c.DriverId);
    }
}