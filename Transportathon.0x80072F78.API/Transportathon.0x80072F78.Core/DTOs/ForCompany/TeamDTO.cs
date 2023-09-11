using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportathon._0x80072F78.Core.Entities.ForCompany;

namespace Transportathon._0x80072F78.Core.DTOs.Company;

public class TeamDTO
{
    public Guid CompanyId { get; set; }
    public Entities.ForCompany.Company Company { get; set; }
    public List<Guid> TeamWorkerId { get; set; }
    public List<TeamWorker> TeamWorkers { get; set; }
}