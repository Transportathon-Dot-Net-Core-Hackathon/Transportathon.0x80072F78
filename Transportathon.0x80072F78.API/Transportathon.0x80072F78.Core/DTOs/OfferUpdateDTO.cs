namespace Transportathon._0x80072F78.Core.DTOs;

public class OfferUpdateDTO
{
    public Guid Id { get; set; }
    public Guid TransportationRequestId { get; set; }
    public Guid CompanyId { get; set; }
    public Guid UserId { get; set; }
    public Guid TeamId { get; set; }
    public Guid DriverId { get; set; }
    public DateTime OfferTime { get; set; }
    public decimal Price { get; set; }
    public string? Note { get; set; }
    public Enums.DocumentStatus Status { get; set; }
}