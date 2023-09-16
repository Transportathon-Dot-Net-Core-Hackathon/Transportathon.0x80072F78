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

public class VehicleRepository : AsyncRepository<Vehicle>, IVehicleRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IFilter _filter;
    private readonly IHttpContextData _httpContextData;

    public VehicleRepository(AppDbContext appDbContext, IFilter filter, IHttpContextData httpContextData) : base(appDbContext, filter)
    {
        _appDbContext = appDbContext;
        _filter = filter;
        _httpContextData = httpContextData;
    }

    public async Task<List<Vehicle>> GetAllVehicleAsync(bool relational = true)
    {
        List<Vehicle> vehicleList = new();
        IQueryable<Vehicle> query = _appDbContext.Vehicles.Where(x => x.UserId == Guid.Parse(_httpContextData.UserId)).AsNoTracking();
        var entityType = _appDbContext.Model.FindEntityType(typeof(Vehicle));

        if (relational == true)
        {
            vehicleList = await query.Include(x => x.Driver)
                                       .ToListAsync();
        }
        else
        {
            vehicleList = await query.ToListAsync();
        }

        return vehicleList;
    }

    public async Task<Vehicle> GetVehicleByIdAsync(Guid id)
    {
        Vehicle vehicle = await _appDbContext.Vehicles.AsNoTracking()
                                                              .Where(x => x.Id == id && x.UserId == Guid.Parse(_httpContextData.UserId)).
                                                               Include(x => x.Driver).
                                                               
                                                               FirstOrDefaultAsync();
        if (vehicle == null) return null;
        return vehicle;
    }
}