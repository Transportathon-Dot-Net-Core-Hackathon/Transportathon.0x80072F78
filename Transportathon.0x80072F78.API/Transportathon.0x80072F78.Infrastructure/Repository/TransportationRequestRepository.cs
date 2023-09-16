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

public class TransportationRequestRepository : AsyncRepository<TransportationRequest>, ITransportationRequestRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IFilter _filter;
    private readonly IHttpContextData _httpContextData;

    public TransportationRequestRepository(AppDbContext appDbContext, IFilter filter, IHttpContextData httpContextData) : base(appDbContext, filter)
    {
        _appDbContext = appDbContext;
        _filter = filter;
        _httpContextData = httpContextData;
    }

    public async Task<List<TransportationRequest>> GetAllTransportationRequestAsync(bool relational = true)
    {
        List<TransportationRequest> transportationRequestList = new();
        IQueryable<TransportationRequest> query = _appDbContext.TransportationRequests.Where(x => x.UserId == Guid.Parse(_httpContextData.UserId)).AsNoTracking();
        var entityType = _appDbContext.Model.FindEntityType(typeof(TransportationRequest));

        if (relational == true)
        {
            transportationRequestList = await query.Include(x => x.OutputAddress)
                                       .Include(x => x.DestinationAddress).ToListAsync();
        }
        else
        {
            transportationRequestList = await query.ToListAsync();
        }

        return transportationRequestList;
    }

    public async Task<TransportationRequest> GetTransportationRequestByIdAsync(Guid id)
    {
        TransportationRequest transportationRequest = await _appDbContext.TransportationRequests.AsNoTracking()
                                                              .Where(x => x.Id == id && x.UserId == Guid.Parse(_httpContextData.UserId)).
                                                               Include(x => x.OutputAddress).
                                                               Include(x => x.DestinationAddress).
                                                               FirstOrDefaultAsync();
        if (transportationRequest == null) return null;
        return transportationRequest;
    }
}