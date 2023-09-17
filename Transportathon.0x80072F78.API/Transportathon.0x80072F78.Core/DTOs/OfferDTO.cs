using Transportathon._0x80072F78.Core.DTOs.Identity;
using Transportathon._0x80072F78.Core.DTOs.ForCompany;
using Transportathon._0x80072F78.Core.Enums;

namespace Transportathon._0x80072F78.Core.DTOs;

public class OfferDTO
{
    public Guid Id { get; set; }
    public Guid TransportationRequestId { get; set; }
    public TransportationRequestDTO TransportationRequest { get; set; }
    public Guid CompanyId { get; set; }
    public CompanyDTO Company { get; set; }
    public Guid UserId { get; set; }
    public UserDTO User { get; set; }
    public Guid TeamId { get; set; }
    public TeamDTO Team { get; set; }
    public Guid VehicleId { get; set; }
    public VehicleDTO Vehicle { get; set; }
    public decimal Price { get; set; }
    public string? Note { get; set; }
    public DateTime TransportationDate { get; set; }
    public DateTime OfferTime { get; set; }
    public DocumentStatus Status { get; set; }
    public bool IsCanComment { get; set; }
    public string? CompanyName { get; set; }
}