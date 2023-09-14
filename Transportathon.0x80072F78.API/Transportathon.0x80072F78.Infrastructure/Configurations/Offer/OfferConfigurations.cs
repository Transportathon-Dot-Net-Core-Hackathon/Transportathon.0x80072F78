using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Transportathon._0x80072F78.Infrastructure.Configurations.Offer;

public class OfferConfigurations : IEntityTypeConfiguration<Core.Entities.Offer.Offer>
{
    public void Configure(EntityTypeBuilder<Core.Entities.Offer.Offer> builder)
    {
        builder.ToTable("Offers");
        builder.Property(x => x.TransportationRequestId).IsRequired();
        builder.Property(x => x.CompanyId).IsRequired();
        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.DriverId).IsRequired();
        builder.Property(x => x.TeamId).IsRequired();
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.OfferTime).IsRequired();
        builder.Property(x => x.Price).IsRequired();

        builder.HasOne(c => c.Company).WithMany().HasForeignKey(c => c.CompanyId);
        builder.HasOne(c => c.User).WithMany().HasForeignKey(c => c.UserId);
        builder.HasOne(c => c.Driver).WithMany().HasForeignKey(c => c.DriverId);
        builder.HasOne(c => c.Team).WithMany().HasForeignKey(c => c.TeamId);
        builder.HasOne(c => c.TransportationRequest).WithMany().HasForeignKey(c => c.TransportationRequestId);
    }
}