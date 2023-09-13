using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportathon._0x80072F78.Core.DTOs.ForCompany;

public class TeamWorkerCreateDTO
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
    public string PhoneNumber { get; set; }
    public string EMail { get; set; }
    public string Experience { get; set; }
}