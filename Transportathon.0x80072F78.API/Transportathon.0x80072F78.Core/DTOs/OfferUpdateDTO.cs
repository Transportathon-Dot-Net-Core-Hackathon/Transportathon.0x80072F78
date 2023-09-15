using Transportathon._0x80072F78.Core.Enums;

namespace Transportathon._0x80072F78.Core.DTOs;

public class OfferUpdateDTO
{
    public Guid Id { get; set; }
    public Guid TransportationRequestId { get; set; }
    public Guid CompanyId { get; set; }
    public Guid UserId { get; set; }
    public Guid TeamId { get; set; }
    public Guid VehicleId { get; set; }
    public decimal Price { get; set; }
    public string? Note { get; set; }
    public DateTime TransportationDate { get; set; }
    public DateTime OfferTime { get; set; }
    public DocumentStatus Status { get; set; }
}