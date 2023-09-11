using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportathon._0x80072F78.Core.Entities.ForCompany;

namespace Transportathon._0x80072F78.Core.DTOs.ForCompany;

public class VehicleDTO
{
    public int VehicleType { get; set; }
    public string VehicleLicensePlate { get; set; }
    public string VehicleVolumeCapacity { get; set; }
    public string VehicleWeightCapacity { get; set; }
    public int VehicleStatus { get; set; }
    public Guid DriverId { get; set; }
    public Driver Driver { get; set; }
}