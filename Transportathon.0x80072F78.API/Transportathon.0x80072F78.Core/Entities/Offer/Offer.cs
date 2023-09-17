using Transportathon._0x80072F78.Core.Entities.ForCompany;
using Transportathon._0x80072F78.Core.Entities.Identity;
using Transportathon._0x80072F78.Core.Enums;

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
    public Guid VehicleId { get; set; }
    public Vehicle Vehicle { get; set; }
    public decimal Price { get; set; }
    public string? Note { get; set; }
    public DateTime TransportationDate { get; set; }
    public DateTime OfferTime { get; set; }
    public DocumentStatus Status { get; set; }

    #region Virtual Fields

    public string? CompanyName { get; set; }

    #endregion
}