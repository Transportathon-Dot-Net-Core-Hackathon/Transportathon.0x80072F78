using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportathon._0x80072F78.Core.Entities.ForCompany;

namespace Transportathon._0x80072F78.Core.DTOs.ForCompany;

public class TeamUpdateDTO
{
    public string Name { get; set; }
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public List<TeamWorkerDTO> TeamWorkers { get; set; }
}