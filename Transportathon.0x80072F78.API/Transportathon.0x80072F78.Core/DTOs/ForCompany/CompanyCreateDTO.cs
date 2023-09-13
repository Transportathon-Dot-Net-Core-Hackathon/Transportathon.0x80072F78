using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportathon._0x80072F78.Core.Entities.Identity;

namespace Transportathon._0x80072F78.Core.DTOs.ForCompany;

public class CompanyCreateDTO
{
    public string CompanyName { get; set; }
    public string Title { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Address { get; set; }
    public string Street { get; set; }
    public string Alley { get; set; }
    public string District { get; set; }
    public string BuildingNumber { get; set; }
    public string ApartmentNumber { get; set; }
    public string PostCode { get; set; }
    public string VKN { get; set; }
    public Guid CompanyUsersId { get; set; }
}