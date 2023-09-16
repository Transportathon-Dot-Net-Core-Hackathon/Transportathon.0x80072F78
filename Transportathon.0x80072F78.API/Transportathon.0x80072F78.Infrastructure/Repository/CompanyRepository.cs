using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportathon._0x80072F78.Core.Entities;
using Transportathon._0x80072F78.Core.Entities.ForCompany;
using Transportathon._0x80072F78.Core.Repository;
using Transportathon._0x80072F78.Infrastructure.Database;
using Transportathon._0x80072F78.Shared.Interfaces;

namespace Transportathon._0x80072F78.Infrastructure.Repository;

public class CompanyRepository : AsyncRepository<Company> , ICompanyRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IFilter _filter;
    private readonly IHttpContextData _httpContextData;

    public CompanyRepository(AppDbContext appDbContext, IFilter filter, IHttpContextData httpContextData) : base(appDbContext, filter)
    {
        _appDbContext = appDbContext;
        _filter = filter;
        _httpContextData = httpContextData;
    }

    public async Task<List<Company>> GetAllCompanyAsync(bool relational = true)
    {
        List<Company> companyList = new();
        IQueryable<Company> query = _appDbContext.Companies.Where(x => x.CompanyUsersId == Guid.Parse(_httpContextData.UserId)).AsNoTracking();
        var entityType = _appDbContext.Model.FindEntityType(typeof(Company));

        if (relational == true)
        {
            companyList = await query.Include(x => x.CompanyUsers)
                                       .ToListAsync();
        }
        else
        {
            companyList = await query.ToListAsync();
        }

        return companyList;
    }

    public async Task<Company> GetCompanyByIdAsync(Guid id)
    {
        Company company = await _appDbContext.Companies.AsNoTracking()
                                                              .Where(x => x.Id == id && x.CompanyUsersId == Guid.Parse(_httpContextData.UserId)).
                                                               Include(x => x.CompanyUsers).
                                                               
                                                               FirstOrDefaultAsync();
        if (company == null) return null;
        return company;
    }
}