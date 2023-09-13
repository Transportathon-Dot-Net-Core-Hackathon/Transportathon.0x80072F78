using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportathon._0x80072F78.Core.DTOs.ForCompany;

public class TeamCreateDTO
{
    public Guid CompanyId { get; set; }
    public List<Guid> TeamWorkerId { get; set; }
}