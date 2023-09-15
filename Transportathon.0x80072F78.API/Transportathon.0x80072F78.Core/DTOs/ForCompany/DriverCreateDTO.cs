using Transportathon._0x80072F78.Core.Enums;

namespace Transportathon._0x80072F78.Core.DTOs.ForCompany;

public class DriverCreateDTO
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Experience { get; set; }
    public string? PhoneNumber { get; set; }
    public string? EMail { get; set; }
    public int Age { get; set; }
    public List<DrivingLicenseType> DrivingLicenceTypes { get; set; }
}