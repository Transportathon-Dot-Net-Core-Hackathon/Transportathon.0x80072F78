using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Transportathon._0x80072F78.Core.Enums;

namespace Transportathon._0x80072F78.Infrastructure.Configurations.Offer;

public class OfferConfigurations : IEntityTypeConfiguration<Core.Entities.Offer.Offer>
{
    public void Configure(EntityTypeBuilder<Core.Entities.Offer.Offer> builder)
    {
        builder.ToTable("Offers");
        builder.Property(x => x.TransportationRequestId).IsRequired();
        builder.Property(x => x.CompanyId).IsRequired();
        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.VehicleId).IsRequired();
        builder.Property(x => x.TeamId).IsRequired();
        builder.Property(x => x.Price).IsRequired();
        builder.Property(x => x.Note).HasMaxLength(500);
        builder.Property(x => x.TransportationDate).IsRequired();
        builder.Property(x => x.OfferTime).IsRequired();
        builder.Property(x => x.Status).IsRequired();

        builder.HasOne(c => c.Company).WithMany().HasForeignKey(c => c.CompanyId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(c => c.User).WithMany().HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(c => c.Vehicle).WithMany().HasForeignKey(c => c.VehicleId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(c => c.Team).WithMany().HasForeignKey(c => c.TeamId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(c => c.TransportationRequest).WithMany().HasForeignKey(c => c.TransportationRequestId).OnDelete(DeleteBehavior.NoAction);
    }
}