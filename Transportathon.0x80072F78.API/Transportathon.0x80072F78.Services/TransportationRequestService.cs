using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportathon._0x80072F78.Core.DTOs;
using Transportathon._0x80072F78.Core.DTOs.Company;
using Transportathon._0x80072F78.Core.DTOs.ForCompany;
using Transportathon._0x80072F78.Core.Entities;
using Transportathon._0x80072F78.Core.Entities.ForCompany;
using Transportathon._0x80072F78.Core.Entities.Identity;
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
        var outputAddress = await _unitOfWork.AddressRepository.AnyAsync(x => x.Id == transportationRequestCreateDTO.OutputAddressId);
        if (!outputAddress)
        {
            return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(Address));
        }
        var destinationAddress = await _unitOfWork.AddressRepository.AnyAsync(x => x.Id == transportationRequestCreateDTO.DestinationAddressId);
        if (!destinationAddress)
        {
            return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(Address));
        }
        var user = await _userManager.FindByIdAsync(_httpContextData.UserId);
        if (user == null)
        {
            return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(AspNetUser));
        }
        var mappedTransportationRequest = _mapper.Map<TransportationRequest>(transportationRequestCreateDTO);
        await _unitOfWork.TransportationRequestRepository.CreateAsync(mappedTransportationRequest);
        await _unitOfWork.SaveAsync();
        return CustomResponse<NoContent>.Success(StatusCodes.Status200OK);
    }

    public async Task<CustomResponse<NoContent>> DeleteAsync(Guid id)
    {
        var transportationRequest = await _unitOfWork.TransportationRequestRepository.GetByIdAsync(id);
        if (transportationRequest == null)
        {
            return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(transportationRequest));
        }
        await _unitOfWork.TransportationRequestRepository.DeleteAsync(transportationRequest);
        await _unitOfWork.SaveAsync();
        return CustomResponse<NoContent>.Success(StatusCodes.Status200OK);
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
        {
            return CustomResponse<TransportationRequestDTO>.Fail(StatusCodes.Status404NotFound, nameof(transportationRequest));
        }
        var transportationRequestDTO = _mapper.Map<TransportationRequestDTO>(transportationRequest);
        return CustomResponse<TransportationRequestDTO>.Success(StatusCodes.Status200OK, transportationRequestDTO);
    }

    public async Task<CustomResponse<List<TransportationRequestDTO>>> MyTransportationRequestsAsync()
    {
        var transportationRequestList = await _unitOfWork.TransportationRequestRepository.GetAllByFilterAsync(x => x.UserId == Guid.Parse(_httpContextData.UserId)
                                                    , null, $"{nameof(Core.Entities.TransportationRequest.OutputAddress)},{nameof(Core.Entities.TransportationRequest.DestinationAddress)},{nameof(Core.Entities.TransportationRequest.User)}");
        if (transportationRequestList == null)
            return CustomResponse<List<TransportationRequestDTO>>.Fail(StatusCodes.Status404NotFound, nameof(Core.Entities.TransportationRequest));

        var result = _mapper.Map<List<TransportationRequestDTO>>(transportationRequestList);

        return CustomResponse<List<TransportationRequestDTO>>.Success(StatusCodes.Status200OK, result);
    }

    public async Task<CustomResponse<NoContent>> UpdateAsync(TransportationRequestUpdateDTO transportationRequestUpdateDTO)
    {
        var transportationRequest = await _unitOfWork.TransportationRequestRepository.AnyAsync(x => x.Id == transportationRequestUpdateDTO.Id);
        if (!transportationRequest)
        {
            return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(transportationRequest));
        }
        var outputAddress = await _unitOfWork.AddressRepository.AnyAsync(x => x.Id == transportationRequestUpdateDTO.OutputAddressId);
        if (!outputAddress)
        {
            return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(outputAddress));
        }
        var destinationAddress = await _unitOfWork.AddressRepository.AnyAsync(x => x.Id == transportationRequestUpdateDTO.DestinationAddressId);
        if (!destinationAddress)
        {
            return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(destinationAddress));
        }
        var user = await _userManager.FindByIdAsync(_httpContextData.UserId);
        if (user == null)
        {
            return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(AspNetUser));
        }
        var result = _mapper.Map<TransportationRequest>(transportationRequestUpdateDTO);
        await _unitOfWork.TransportationRequestRepository.UpdateAsync(result);
        await _unitOfWork.SaveAsync();
        return CustomResponse<NoContent>.Success(StatusCodes.Status200OK);
    }
}