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

public class DriverRepository : AsyncRepository<Driver>, IDriverRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IFilter _filter;
    private readonly IHttpContextData _httpContextData;

    public DriverRepository(AppDbContext appDbContext, IFilter filter, IHttpContextData httpContextData) : base(appDbContext, filter)
    {
        _appDbContext = appDbContext;
        _filter = filter;
        _httpContextData = httpContextData;
    }

    public async Task<List<Driver>> GetAllDriverAsync(bool relational = true)
    {
        List<Driver> driverList = new();
        IQueryable<Driver> query = _appDbContext.Drivers.Where(x => x.UserId == Guid.Parse(_httpContextData.UserId)).AsNoTracking();
        var entityType = _appDbContext.Model.FindEntityType(typeof(Driver));

        if (relational == true)
        {
            driverList = await query.ToListAsync();
        }
        else
        {
            driverList = await query.ToListAsync();
        }

        return driverList;
    }

    public async Task<Driver> GetDriverByIdAsync(Guid id)
    {
        Driver driver = await _appDbContext.Drivers.AsNoTracking()
                                                              .Where(x => x.Id == id && x.UserId == Guid.Parse(_httpContextData.UserId)).
                                                               
                                                               FirstOrDefaultAsync();
        if (driver == null) return null;
        return driver;
    }
}