using Transportathon._0x80072F78.Core.Entities.ForCompany;
using Transportathon._0x80072F78.Core.Entities.Identity;

namespace Transportathon._0x80072F78.Core.Entities.Offer;

public class Offer : BaseEntity<Guid>
{
    public Guid TransportationRequestId { get; set; }
    public TransportationRequest TransportationRequest { get; set; }
    public Guid CompanyId { get; set; }
    public Company Company { get; set; }
    public Guid UserId { get; set; }
    public AspNetUser User { get; set; }
    public Guid TeamId { get; set; }
    public Team Team { get; set; }
    public Guid DriverId { get; set; }
    public Driver Driver { get; set; }
    public DateTime OfferTime { get; set; }
    public decimal Price { get; set; }
    public string? Note { get; set; }
    public int Status { get; set; }
}