using Transportathon._0x80072F78.Core.Enums;

namespace Transportathon._0x80072F78.Core.DTOs.ForCompany;

public class VehicleCreateDTO
{
    public VehicleType VehicleType { get; set; }
    public string VehicleLicensePlate { get; set; }
    public string? VehicleVolumeCapacity { get; set; }
    public string? VehicleWeightCapacity { get; set; }
    public VehicleStatus VehicleStatus { get; set; }
    public Guid DriverId { get; set; }
}