using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportathon._0x80072F78.Core.Entities.ForCompany;

public class TeamWorker : BaseEntity<Guid>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
    public string? PhoneNumber { get; set; }
    public string? EMail { get; set; }
    public string? Experience { get; set; }
    public Guid TeamId { get; set; }
    public Team Team { get; set; }
    public Guid UserId { get; set; }
}