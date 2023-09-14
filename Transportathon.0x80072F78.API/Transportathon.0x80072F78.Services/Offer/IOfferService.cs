using Transportathon._0x80072F78.Core.DTOs;
using Transportathon._0x80072F78.Shared.Models;

namespace Transportathon._0x80072F78.Services.Offer;

public interface IOfferService
{
    Task<CustomResponse<List<OfferDTO>>> GetAllAsync(bool relational);
    Task<CustomResponse<NoContent>> DeleteAsync(Guid id);
    Task<CustomResponse<NoContent>> CreateAsync(OfferCreateDTO offerCreateDTO);
    Task<CustomResponse<NoContent>> UpdateAsync(OfferUpdateDTO offerUpdateDTO);
    Task<CustomResponse<OfferDTO>> GetByIdAsync(Guid id);
    Task<CustomResponse<List<OfferDTO>>> MyOffersAsync();
}