using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportathon._0x80072F78.Core.Entities;
using Transportathon._0x80072F78.Core.Entities.ForCompany;
using Transportathon._0x80072F78.Shared.Interfaces;

namespace Transportathon._0x80072F78.Core.Repository;

public interface ICompanyRepository : IAsyncRepository<Company>
{
    Task<List<Company>> GetAllCompanyAsync(bool relational);
    Task<Company> GetCompanyByIdAsync(Guid id);
}
