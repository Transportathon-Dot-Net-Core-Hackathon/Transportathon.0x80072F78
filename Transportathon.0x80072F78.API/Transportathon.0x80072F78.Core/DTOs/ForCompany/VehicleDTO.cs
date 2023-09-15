using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportathon._0x80072F78.Core.Entities.ForCompany;
using Transportathon._0x80072F78.Core.Enums;

namespace Transportathon._0x80072F78.Core.DTOs.ForCompany;

public class VehicleDTO
{
    public Guid Id { get; set; }
    public VehicleType VehicleType { get; set; }
    public string VehicleLicensePlate { get; set; }
    public string? VehicleVolumeCapacity { get; set; }
    public string? VehicleWeightCapacity { get; set; }
    public VehicleStatus VehicleStatus { get; set; }
    public Guid DriverId { get; set; }
    public DriverDTO Driver { get; set; }
}