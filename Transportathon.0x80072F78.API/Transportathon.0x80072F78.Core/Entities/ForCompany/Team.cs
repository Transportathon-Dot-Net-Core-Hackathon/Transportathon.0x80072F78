using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportathon._0x80072F78.Core.Entities.ForCompany;

public class Team : BaseEntity<Guid>
{
    public Guid CompanyId { get; set; }
    public Company Company { get; set; }
    public List<Guid> TeamWorkerId { get; set; }
    public List<TeamWorker> TeamWorkers { get; set; }
}