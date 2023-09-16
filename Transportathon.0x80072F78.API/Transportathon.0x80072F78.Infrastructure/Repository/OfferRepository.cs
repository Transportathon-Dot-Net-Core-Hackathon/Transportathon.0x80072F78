using Microsoft.EntityFrameworkCore;
using Transportathon._0x80072F78.Core.Entities.Offer;
using Transportathon._0x80072F78.Core.Repository;
using Transportathon._0x80072F78.Infrastructure.Database;
using Transportathon._0x80072F78.Shared.Interfaces;

namespace Transportathon._0x80072F78.Infrastructure.Repository;

public class OfferRepository : AsyncRepository<Offer>, IOfferRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IFilter _filter;
    private readonly IHttpContextData _httpContextData;

    public OfferRepository(AppDbContext appDbContext, IFilter filter, IHttpContextData httpContextData) : base(appDbContext, filter)
    {
        _appDbContext = appDbContext;
        _filter = filter;
        _httpContextData = httpContextData;
    }

    public async Task<List<Offer>> GetAllOffersAsync(bool relational = true)
    {
        List<Offer> offerList = new();
        IQueryable<Offer> query = _appDbContext.Offers.Where(x=>x.UserId == Guid.Parse(_httpContextData.UserId)).AsNoTracking();
        var entityType = _appDbContext.Model.FindEntityType(typeof(Offer));

        if (relational == true)
        {
            offerList = await query.Include(x => x.TransportationRequest)
                                        .Include(x => x.Company)
                                        .Include(x => x.User)
                                        .Include(x => x.Team)
                                        .Include(x => x.Vehicle).ToListAsync();
        }
        else
        {
            offerList = await query.ToListAsync();
        }

        return offerList;
    }

    public async Task<Offer> GetOfferByIdAsync(Guid id)
    {
        Offer offerRequest = await _appDbContext.Offers.AsNoTracking()
                                                                .Where(x => x.Id == id && x.UserId == Guid.Parse(_httpContextData.UserId))
                                                                .Include(x => x.TransportationRequest)
                                                                .Include(x => x.Company)
                                                                .Include(x => x.User)
                                                                .Include(x => x.Team)
                                                                .Include(x => x.Vehicle).FirstOrDefaultAsync();
        if (offerRequest == null) return null;
        return offerRequest;
    }
}