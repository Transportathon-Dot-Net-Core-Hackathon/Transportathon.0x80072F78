﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportathon._0x80072F78.Core.Entities.Company;

public class Driver : BaseEntity<Guid>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Experience { get; set; }
    public string PhoneNumber { get; set; }
    public string EMail { get; set; }
    public int Age { get; set; }
    public List<int> DrivingLicenceTypes { get; set; }
}