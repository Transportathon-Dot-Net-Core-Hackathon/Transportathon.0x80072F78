using AutoMapper;
using Microsoft.AspNetCore.Http;
using Transportathon._0x80072F78.Core.DTOs;
using Transportathon._0x80072F78.Core.Enums;
using Transportathon._0x80072F78.Core.Repository;
using Transportathon._0x80072F78.Shared.Interfaces;
using Transportathon._0x80072F78.Shared.Models;

namespace Transportathon._0x80072F78.Services.Offer;

public class OfferService : IOfferService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextData _httpContextData;
    public OfferService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextData httpContextData)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextData = httpContextData;
    }

    public async Task<CustomResponse<NoContent>> CreateAsync(OfferCreateDTO offerCreateDTO)
    {
        try
        {
            var transportationRequest = await _unitOfWork.TransportationRequestRepository.AnyAsync(x => x.Id == offerCreateDTO.TransportationRequestId);
            if (!transportationRequest)
            {
                return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(transportationRequest));
            }
            await _unitOfWork.BeginTransactionAsync();

            var mappedOffer = _mapper.Map<Core.Entities.Offer.Offer>(offerCreateDTO);
            mappedOffer.UserId = Guid.Parse(_httpContextData.UserId);
            mappedOffer.OfferTime = DateTime.Now;
            mappedOffer.Status = DocumentStatus.Pending;

            await _unitOfWork.OfferRepository.CreateAsync(mappedOffer);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitAsync();

            return CustomResponse<NoContent>.Success(StatusCodes.Status200OK);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            return CustomResponse<NoContent>.Fail(StatusCodes.Status400BadRequest, new List<string> { ex.Message });
        }
    }

    public async Task<CustomResponse<NoContent>> DeleteAsync(Guid id)
    {
        try
        {
            var offer = await _unitOfWork.OfferRepository.GetByIdAsync(id);
            if (offer == null)
                return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(Core.Entities.Offer.Offer));

            await _unitOfWork.BeginTransactionAsync();

            await _unitOfWork.OfferRepository.DeleteAsync(offer);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitAsync();

            return CustomResponse<NoContent>.Success(StatusCodes.Status200OK);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            return CustomResponse<NoContent>.Fail(StatusCodes.Status400BadRequest, new List<string> { ex.Message });
        }
    }

    public async Task<CustomResponse<List<OfferDTO>>> GetAllAsync(bool relational)
    {
        var offerList = await _unitOfWork.OfferRepository.GetAllOffersAsync(relational);

        var offerDtoList = _mapper.Map<List<OfferDTO>>(offerList);

        return CustomResponse<List<OfferDTO>>.Success(StatusCodes.Status200OK, offerDtoList);
    }

    public async Task<CustomResponse<OfferDTO>> GetByIdAsync(Guid id)
    {
        var offer = await _unitOfWork.OfferRepository.GetOfferByIdAsync(id);
        if (offer == null)
            return CustomResponse<OfferDTO>.Fail(StatusCodes.Status404NotFound, nameof(Core.Entities.Offer.Offer));

        string companyName = (await _unitOfWork.CompanyRepository.GetAllByFilterAsync(x => x.Id == offer.CompanyId, null, null, t => t.CompanyName)).SingleOrDefault();

        if (offer.Status == DocumentStatus.Approved)
            offer = await _unitOfWork.OfferRepository.GetOfferByIdIfApprovedAsync(id);

        var offerDTO = _mapper.Map<OfferDTO>(offer);
        offerDTO.CompanyName = companyName;
        offerDTO.IsCanComment = await IsCanCommentAsync(id);

        return CustomResponse<OfferDTO>.Success(StatusCodes.Status200OK, offerDTO);
    }

    public async Task<CustomResponse<NoContent>> UpdateAsync(OfferUpdateDTO offerUpdateDTO)
    {
        try
        {
            var offer = await _unitOfWork.OfferRepository.AnyAsync(x => x.Id == offerUpdateDTO.Id);
            if (!offer)
                return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(offer));

            await _unitOfWork.BeginTransactionAsync();

            var result = _mapper.Map<Core.Entities.Offer.Offer>(offerUpdateDTO);
            result.UserId = Guid.Parse(_httpContextData.UserId);

            await _unitOfWork.OfferRepository.UpdateAsync(result);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitAsync();

            return CustomResponse<NoContent>.Success(StatusCodes.Status200OK);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            return CustomResponse<NoContent>.Fail(StatusCodes.Status400BadRequest, new List<string> { ex.Message });
        }
    }

    public async Task<CustomResponse<List<OfferDTO>>> MyOffersAsync()
    {
        var offerList = await _unitOfWork.OfferRepository.GetAllByFilterAsync(x => x.UserId == Guid.Parse(_httpContextData.UserId)
                                                    ,null, $"{nameof(Core.Entities.Offer.Offer.Company)},{nameof(Core.Entities.Offer.Offer.User)},{nameof(Core.Entities.Offer.Offer.Team)},{nameof(Core.Entities.Offer.Offer.Vehicle)},{nameof(Core.Entities.Offer.Offer.TransportationRequest)}");
        if (offerList == null)
            return CustomResponse<List<OfferDTO>>.Fail(StatusCodes.Status404NotFound, nameof(Core.Entities.Offer.Offer));

        var result = _mapper.Map<List<OfferDTO>>(offerList);

        return CustomResponse<List<OfferDTO>>.Success(StatusCodes.Status200OK, result);
    }

    private async Task<bool> IsCanCommentAsync(Guid offerId)
    {
        bool checkComment = await _unitOfWork.CommentRepository.AnyAsync(x=> x.OfferId == offerId);
        if (checkComment)
            return false;

        return true;
    }

    public async Task<CustomResponse<OfferDTO>> ChangeOfferStatusAsync(StatusUpdateDTO statusUpdateDTO)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            Core.Entities.Offer.Offer offer = await _unitOfWork.OfferRepository.GetOfferByIdAsync(statusUpdateDTO.Id);
            if (offer == null)
                return CustomResponse<OfferDTO>.Fail(StatusCodes.Status400BadRequest, nameof(Core.Entities.Offer.Offer));

            string companyName = (await _unitOfWork.CompanyRepository.GetAllByFilterAsync(x => x.Id == offer.CompanyId, null, null, t => t.CompanyName)).SingleOrDefault();

            if (offer.Status == DocumentStatus.Approved)
                offer = await _unitOfWork.OfferRepository.GetOfferByIdIfApprovedAsync(statusUpdateDTO.Id);
            
            offer.Status = statusUpdateDTO.DocumentStatus;
            await _unitOfWork.OfferRepository.UpdateAsync(offer);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitAsync();

            OfferDTO offerDTO = _mapper.Map<OfferDTO>(offer);
            offerDTO.CompanyName = companyName;
            offerDTO.IsCanComment = await IsCanCommentAsync(statusUpdateDTO.Id);

            return CustomResponse<OfferDTO>.Success(StatusCodes.Status200OK, offerDTO);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            return CustomResponse<OfferDTO>.Fail(StatusCodes.Status400BadRequest, new List<string> { ex.Message });
        }
    }
}