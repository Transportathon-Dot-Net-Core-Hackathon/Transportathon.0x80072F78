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

    public OfferRepository(AppDbContext appDbContext, IFilter filter) : base(appDbContext, filter)
    {
        _appDbContext = appDbContext;
        _filter = filter;
    }

    public async Task<List<Offer>> GetAllOffersAsync(bool relational = false)
    {
        List<Offer> offerList = new();
        IQueryable<Offer> query = _appDbContext.Offers.AsNoTracking();
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
                                                                .Where(x => x.Id == id)
                                                                .Include(x => x.TransportationRequest)
                                                                .Include(x => x.Company)
                                                                .Include(x => x.User)
                                                                .Include(x => x.Team)
                                                                .Include(x => x.Vehicle).FirstOrDefaultAsync();
        if (offerRequest == null) return null;
        return offerRequest;
    }
}