using Transportathon._0x80072F78.Core.Enums;

namespace Transportathon._0x80072F78.Core.DTOs;

public class AddressCreateDTO
{
    public AddressType AddressType { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public string LocalAddress { get; set; }
}