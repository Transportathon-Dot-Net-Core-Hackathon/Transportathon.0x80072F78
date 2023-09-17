using Transportathon._0x80072F78.Core.Entities.Offer;
using Transportathon._0x80072F78.Shared.Interfaces;

namespace Transportathon._0x80072F78.Core.Repository;

public interface IOfferRepository :IAsyncRepository<Offer>
{
    Task<List<Offer>> GetAllOffersAsync(bool relational);
    Task<Offer> GetOfferByIdAsync(Guid id);
    Task<Offer> GetOfferByIdIfApprovedAsync(Guid id);
}