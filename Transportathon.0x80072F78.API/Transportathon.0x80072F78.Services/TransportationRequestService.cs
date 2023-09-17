using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Transportathon._0x80072F78.Core.DTOs;
using Transportathon._0x80072F78.Core.Entities;
using Transportathon._0x80072F78.Core.Entities.Identity;
using Transportathon._0x80072F78.Core.Enums;
using Transportathon._0x80072F78.Core.Repository;
using Transportathon._0x80072F78.Shared.Interfaces;
using Transportathon._0x80072F78.Shared.Models;

namespace Transportathon._0x80072F78.Services;

public class TransportationRequestService : ITransportationRequestService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextData _httpContextData;
    private readonly UserManager<AspNetUser> _userManager;

    public TransportationRequestService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextData httpContextData, UserManager<AspNetUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextData = httpContextData;
        _userManager = userManager;
    }

    public async Task<CustomResponse<NoContent>> CreateAsync(TransportationRequestCreateDTO transportationRequestCreateDTO)
    {
        try
        {
            var outputAddress = await _unitOfWork.AddressRepository.AnyAsync(x => x.Id == transportationRequestCreateDTO.OutputAddressId);
            if (!outputAddress)
                return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(Address));

            var destinationAddress = await _unitOfWork.AddressRepository.AnyAsync(x => x.Id == transportationRequestCreateDTO.DestinationAddressId);
            if (!destinationAddress)
                return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(Address));

            var user = await _userManager.FindByIdAsync(_httpContextData.UserId);
            if (user == null)
                return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(AspNetUser));
            await _unitOfWork.BeginTransactionAsync();

            var mappedTransportationRequest = _mapper.Map<TransportationRequest>(transportationRequestCreateDTO);
            mappedTransportationRequest.UserId = Guid.Parse(_httpContextData.UserId);
            mappedTransportationRequest.CreatedDate = DateTime.Now;
            mappedTransportationRequest.DocumentStatus = DocumentStatus.Pending;

            await _unitOfWork.TransportationRequestRepository.CreateAsync(mappedTransportationRequest);
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
            var transportationRequest = await _unitOfWork.TransportationRequestRepository.GetByIdAsync(id);
            if (transportationRequest == null)
                return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(transportationRequest));

            await _unitOfWork.BeginTransactionAsync();

            await _unitOfWork.TransportationRequestRepository.DeleteAsync(transportationRequest);
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

    public async Task<CustomResponse<List<TransportationRequestDTO>>> GetAllAsync(bool relational)
    {
        var transportationRequestList = await _unitOfWork.TransportationRequestRepository.GetAllTransportationRequestAsync(relational);

        return CustomResponse<List<TransportationRequestDTO>>.Success(StatusCodes.Status200OK, _mapper.Map<List<TransportationRequestDTO>>(transportationRequestList));
    }

    public async Task<CustomResponse<TransportationRequestDTO>> GetByIdAsync(Guid id)
    {
        var transportationRequest = await _unitOfWork.TransportationRequestRepository.GetTransportationRequestByIdAsync(id);
        if (transportationRequest == null)
            return CustomResponse<TransportationRequestDTO>.Fail(StatusCodes.Status404NotFound, nameof(transportationRequest));
        
        var transportationRequestDTO = _mapper.Map<TransportationRequestDTO>(transportationRequest);

        return CustomResponse<TransportationRequestDTO>.Success(StatusCodes.Status200OK, transportationRequestDTO);
    }

    public async Task<CustomResponse<List<TransportationRequestDTO>>> MyTransportationRequestsAsync()
    {
        var transportationRequestList = await _unitOfWork.TransportationRequestRepository.GetAllByFilterAsync(x => x.UserId == Guid.Parse(_httpContextData.UserId)
                                                    , null, $"{nameof(TransportationRequest.OutputAddress)},{nameof(TransportationRequest.DestinationAddress)},{nameof(TransportationRequest.User)}");
        if (transportationRequestList == null)
            return CustomResponse<List<TransportationRequestDTO>>.Fail(StatusCodes.Status404NotFound, nameof(Core.Entities.TransportationRequest));

        var result = _mapper.Map<List<TransportationRequestDTO>>(transportationRequestList);

        return CustomResponse<List<TransportationRequestDTO>>.Success(StatusCodes.Status200OK, result);
    }

    public async Task<CustomResponse<NoContent>> UpdateAsync(TransportationRequestUpdateDTO transportationRequestUpdateDTO)
    {
        try
        {
            var transportationRequest = await _unitOfWork.TransportationRequestRepository.AnyAsync(x => x.Id == transportationRequestUpdateDTO.Id);
            if (!transportationRequest)
                return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(transportationRequest));

            var outputAddress = await _unitOfWork.AddressRepository.AnyAsync(x => x.Id == transportationRequestUpdateDTO.OutputAddressId);
            if (!outputAddress)
                return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(outputAddress));

            var destinationAddress = await _unitOfWork.AddressRepository.AnyAsync(x => x.Id == transportationRequestUpdateDTO.DestinationAddressId);
            if (!destinationAddress)
                return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(destinationAddress));

            var user = await _userManager.FindByIdAsync(_httpContextData.UserId);
            if (user == null)
                return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(AspNetUser));

            await _unitOfWork.BeginTransactionAsync();

            var result = _mapper.Map<TransportationRequest>(transportationRequestUpdateDTO);

            await _unitOfWork.TransportationRequestRepository.UpdateAsync(result);
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

    public async Task<CustomResponse<TransportationRequestDTO>> ChangeTransportationRequestStatusAsync(StatusUpdateDTO statusUpdateDTO)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            TransportationRequest transportationRequest = await _unitOfWork.TransportationRequestRepository.GetByIdAsync(statusUpdateDTO.Id);
            if (transportationRequest == null)
                return CustomResponse<TransportationRequestDTO>.Fail(StatusCodes.Status400BadRequest, nameof(TransportationRequest));

            transportationRequest.DocumentStatus = statusUpdateDTO.DocumentStatus;

            await _unitOfWork.TransportationRequestRepository.UpdateAsync(transportationRequest);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitAsync();

            TransportationRequestDTO transportationRequestDTO = _mapper.Map<TransportationRequestDTO>(transportationRequest);

            return CustomResponse<TransportationRequestDTO>.Success(StatusCodes.Status200OK, transportationRequestDTO);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            return CustomResponse<TransportationRequestDTO>.Fail(StatusCodes.Status400BadRequest, new List<string> { ex.Message });
        }
    }
}