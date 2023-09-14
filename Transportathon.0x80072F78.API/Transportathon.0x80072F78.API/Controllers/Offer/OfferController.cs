using Microsoft.AspNetCore.Mvc;
using Transportathon._0x80072F78.Core.DTOs;
using Transportathon._0x80072F78.Services.Offer;
using Transportathon._0x80072F78.Shared.Models;

namespace Transportathon._0x80072F78.API.Controllers.Offer;

public class OfferController : CustomBaseController
{
    private readonly IOfferService _offerService;

    public OfferController(IOfferService offerService)
    {
        _offerService = offerService;
    }

    [HttpPost]
    public async Task<ActionResult<CustomResponse<NoContent>>> Create(OfferCreateDTO offerCreateDTO)
    {
        return CreateActionResultInstance(await _offerService.CreateAsync(offerCreateDTO));
    }

    [HttpPut]
    public async Task<ActionResult<CustomResponse<NoContent>>> Update(OfferUpdateDTO offerUpdateDTO)
    {
        return CreateActionResultInstance(await _offerService.UpdateAsync(offerUpdateDTO));
    }

    [HttpDelete]
    public async Task<ActionResult<CustomResponse<NoContent>>> Delete(Guid id)
    {
        return CreateActionResultInstance(await _offerService.DeleteAsync(id));
    }

    [HttpGet]
    public async Task<ActionResult<CustomResponse<List<OfferDTO>>>> GetAll(bool relational)
    {
        return CreateActionResultInstance(await _offerService.GetAllAsync(relational));
    }

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<CustomResponse<OfferDTO>>> GetOffer(Guid id)
    {
        return CreateActionResultInstance(await _offerService.GetByIdAsync(id));
    }

    [HttpGet]
    public async Task<ActionResult<CustomResponse<List<OfferDTO>>>> MyOffers()
    {
        return CreateActionResultInstance(await _offerService.MyOffersAsync());
    }
}