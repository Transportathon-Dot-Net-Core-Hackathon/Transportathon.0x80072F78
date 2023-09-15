using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transportathon._0x80072F78.Core.Entities;

namespace Transportathon._0x80072F78.Infrastructure.Configurations;

public class TransportationRequestConfiguration : IEntityTypeConfiguration<Core.Entities.TransportationRequest>
{
    public void Configure(EntityTypeBuilder<TransportationRequest> builder)
    {
        builder.ToTable("TransportationRequests");
        builder.Property(x => x.RequestType).IsRequired();
        builder.Property(x => x.OutputAddressId).IsRequired();
        builder.Property(x => x.DestinationAddressId).IsRequired();
        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.Note).HasMaxLength(500);
        builder.Property(x => x.FirstDateRange).IsRequired();
        builder.Property(x => x.CreatedDate).IsRequired();
        builder.Property(x => x.DocumentStatus).IsRequired();

        builder.HasOne(c => c.OutputAddress).WithMany().HasForeignKey(c => c.OutputAddressId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(c => c.DestinationAddress).WithMany().HasForeignKey(c => c.DestinationAddressId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(c => c.User).WithMany().HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.NoAction);
    }
}