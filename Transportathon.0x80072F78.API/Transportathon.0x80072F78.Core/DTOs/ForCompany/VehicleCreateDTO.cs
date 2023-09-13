﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportathon._0x80072F78.Core.DTOs.ForCompany;

public class VehicleCreateDTO
{
    public Guid Id { get; set; }
    public int VehicleType { get; set; }
    public string VehicleLicensePlate { get; set; }
    public string VehicleVolumeCapacity { get; set; }
    public string VehicleWeightCapacity { get; set; }
    public int VehicleStatus { get; set; }
    public Guid DriverId { get; set; }
}