using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportathon._0x80072F78.Core.Enums;

public enum DrivingLicenseType
{
    B = 0,
    C = 1,
    CE = 2
}
public enum VehicleType
{
    PickupTruck = 0,
    Truck = 1,
}
public enum VehicleStatus
{
    Available = 0,
    BeingUsed = 1,
    InMaintenance = 2,
    OutOfUse = 3
}
public enum TransportationType
{
    HomeToHome = 0,
    OfficeTransportation = 1,
    LargeVolume = 2,
    HeavyGoods =3
}
public enum AddressType
{
    OutputAddress =0,
    DestinationAddress = 1
}
public enum DocumentStatus
{
    Pending = 0,
    Approved = 1,
    Completed = 2,
    Canceled = 3,
    Rejected = 4
}