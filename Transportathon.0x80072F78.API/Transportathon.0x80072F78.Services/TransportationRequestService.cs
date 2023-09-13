using AutoMapper;
using Microsoft.AspNetCore.Http;
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
using Transportathon._0x80072F78.Core.Repository;
using Transportathon._0x80072F78.Shared.Models;

namespace Transportathon._0x80072F78.Services;

public class TransportationRequestService : ITransportationRequestService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TransportationRequestService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CustomResponse<NoContent>> CreateAsync(TransportationRequestCreateDTO transportationRequestCreateDTO)
    {
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

    public async Task<CustomResponse<List<TransportationRequestDTO>>> GetAllAsync()
    {
        var transportationRequestList = await _unitOfWork.TransportationRequestRepository.GetAllAsync();
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

    public async Task<CustomResponse<NoContent>> UpdateAsync(TransportationRequestUpdateDTO transportationRequestUpdateDTO)
    {
        var transportationRequest = await _unitOfWork.TransportationRequestRepository.AnyAsync(x => x.Id == transportationRequestUpdateDTO.Id);
        if (!transportationRequest)
        {
            return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(transportationRequest));
        }
        var result = _mapper.Map<TransportationRequest>(transportationRequestUpdateDTO);
        await _unitOfWork.TransportationRequestRepository.UpdateAsync(result);
        await _unitOfWork.SaveAsync();
        return CustomResponse<NoContent>.Success(StatusCodes.Status200OK);
    }
}