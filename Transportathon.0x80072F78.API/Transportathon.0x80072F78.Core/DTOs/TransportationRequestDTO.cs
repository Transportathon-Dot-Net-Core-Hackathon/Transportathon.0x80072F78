using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportathon._0x80072F78.Core.Entities.Identity;
using Transportathon._0x80072F78.Core.Entities;

namespace Transportathon._0x80072F78.Core.DTOs;

public class TransportationRequestDTO
{
    public int RequestType { get; set; }
    public Guid OutputAddressId { get; set; }
    public Address OutputAddress { get; set; }
    public Guid DestinationAddressId { get; set; }
    public Address DestinationAddress { get; set; }
    public Guid UserId { get; set; }
    public AspNetUser User { get; set; }
    public float Weight { get; set; }
    public float Volume { get; set; }
    public string Note { get; set; }
    public DateTime CreatedDate { get; set; }
    public int DocumentStatus { get; set; }
}