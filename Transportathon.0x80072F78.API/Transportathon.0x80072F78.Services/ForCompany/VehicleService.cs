using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportathon._0x80072F78.Core.DTOs.Company;
using Transportathon._0x80072F78.Core.DTOs.ForCompany;
using Transportathon._0x80072F78.Core.Entities.ForCompany;
using Transportathon._0x80072F78.Core.Repository;
using Transportathon._0x80072F78.Shared.Models;

namespace Transportathon._0x80072F78.Services.ForCompany;

public class VehicleService : IVehicleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public VehicleService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CustomResponse<NoContent>> CreateAsync(VehicleCreateDTO vehicleCreateDTO)
    {
        var mappedVehicle = _mapper.Map<Vehicle>(vehicleCreateDTO);
        await _unitOfWork.VehicleRepository.CreateAsync(mappedVehicle);
        await _unitOfWork.SaveAsync();
        return CustomResponse<NoContent>.Success(StatusCodes.Status200OK);
    }

    public async Task<CustomResponse<NoContent>> DeleteAsync(Guid id)
    {
        var vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(id);
        if (vehicle == null)
        {
            return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(vehicle));
        }
        await _unitOfWork.VehicleRepository.DeleteAsync(vehicle);
        await _unitOfWork.SaveAsync();
        return CustomResponse<NoContent>.Success(StatusCodes.Status200OK);
    }

    public async Task<CustomResponse<List<VehicleDTO>>> GetAllAsync()
    {
        var vehicleList = await _unitOfWork.VehicleRepository.GetAllAsync();
        return CustomResponse<List<VehicleDTO>>.Success(StatusCodes.Status200OK, _mapper.Map<List<VehicleDTO>>(vehicleList));
    }

    public async Task<CustomResponse<VehicleDTO>> GetByIdAsync(Guid id)
    {
         var vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(id);
        if (vehicle == null)
        {
            return CustomResponse<VehicleDTO>.Fail(StatusCodes.Status404NotFound, nameof(vehicle));
        }
        var vehicleDTO = _mapper.Map<VehicleDTO>(vehicle);
        return CustomResponse<VehicleDTO>.Success(StatusCodes.Status200OK, vehicleDTO);
    }

    public async Task<CustomResponse<NoContent>> UpdateAsync(VehicleUpdateDTO vehicleUpdateDTO)
    {
        var vehicle = await _unitOfWork.VehicleRepository.AnyAsync(x => x.Id == vehicleUpdateDTO.Id);
        if (!vehicle)
        {
            return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(vehicle));
        }
        var result = _mapper.Map<Vehicle>(vehicleUpdateDTO);
        await _unitOfWork.VehicleRepository.UpdateAsync(result);
        await _unitOfWork.SaveAsync();
        return CustomResponse<NoContent>.Success(StatusCodes.Status200OK);
    }
}