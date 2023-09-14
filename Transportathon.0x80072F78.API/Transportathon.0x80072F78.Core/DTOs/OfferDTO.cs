using Transportathon._0x80072F78.Core.Entities.ForCompany;
using Transportathon._0x80072F78.Core.Entities.Identity;
using Transportathon._0x80072F78.Core.Entities;
using Transportathon._0x80072F78.Core.DTOs.Identity;
using Transportathon._0x80072F78.Core.DTOs.Company;

namespace Transportathon._0x80072F78.Core.DTOs;

public class OfferDTO
{
    public Guid TransportationRequestId { get; set; }
    public TransportationRequestDTO TransportationRequest { get; set; }
    public Guid CompanyId { get; set; }
    public CompanyDTO Company { get; set; }
    public Guid UserId { get; set; }
    public UserDTO User { get; set; }
    public Guid TeamId { get; set; }
    public TeamDTO Team { get; set; }
    public Guid DriverId { get; set; }
    public DriverDTO Driver { get; set; }
    public DateTime OfferTime { get; set; }
    public decimal Price { get; set; }
    public string? Note { get; set; }
    public int Status { get; set; }
}