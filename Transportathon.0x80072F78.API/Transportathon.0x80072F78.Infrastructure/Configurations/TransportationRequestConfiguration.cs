using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportathon._0x80072F78.Core.Entities;

namespace Transportathon._0x80072F78.Infrastructure.Configurations;

public class TransportationRequestConfiguration : IEntityTypeConfiguration<Core.Entities.TransportationRequest>
{
    public void Configure(EntityTypeBuilder<TransportationRequest> builder)
    {
        builder.ToTable("TransportationRequests");
        builder.Property(x => x.RequestType).IsRequired().HasMaxLength(30);
        builder.Property(x => x.OutputAddressId).IsRequired().HasMaxLength(50);
        builder.Property(x => x.DestinationAddressId).IsRequired().HasMaxLength(50);
        builder.Property(x => x.UserId).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Weight).IsRequired().HasMaxLength(20);
        builder.Property(x => x.Volume).IsRequired().HasMaxLength(20);
        builder.Property(x => x.Note).IsRequired().HasMaxLength(250);
        builder.Property(x => x.CreatedDate).IsRequired().HasMaxLength(20);
        builder.Property(x => x.DocumentStatus).IsRequired().HasMaxLength(10);

        builder.HasOne(c => c.OutputAddress).WithMany().HasForeignKey(c => c.OutputAddressId);
        builder.HasOne(c => c.DestinationAddress).WithMany().HasForeignKey(c => c.DestinationAddressId);
        builder.HasOne(c => c.User).WithMany().HasForeignKey(c => c.UserId);
    }
}