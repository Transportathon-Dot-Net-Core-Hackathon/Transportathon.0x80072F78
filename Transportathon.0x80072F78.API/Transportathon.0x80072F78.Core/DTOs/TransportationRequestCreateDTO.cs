using Transportathon._0x80072F78.Core.Enums;

namespace Transportathon._0x80072F78.Core.DTOs;

public class TransportationRequestCreateDTO
{
    public TransportationType RequestType { get; set; }
    public Guid OutputAddressId { get; set; }
    public Guid DestinationAddressId { get; set; }
    public Guid UserId { get; set; }
    public float? Weight { get; set; }
    public float? Volume { get; set; }
    public string? Note { get; set; }
    public DateTime FirstDateRange { get; set; }
    public DateTime? LastDateRange { get; set; }
    public DateTime CreatedDate { get; set; }
    public DocumentStatus DocumentStatus { get; set; }
}