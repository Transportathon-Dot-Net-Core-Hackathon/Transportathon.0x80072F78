using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Transportathon._0x80072F78.Core.DTOs.ForCompany;
using Transportathon._0x80072F78.Core.Entities.ForCompany;
using Transportathon._0x80072F78.Core.Enums;
using Transportathon._0x80072F78.Core.Repository;
using Transportathon._0x80072F78.Shared.Interfaces;
using Transportathon._0x80072F78.Shared.Models;

namespace Transportathon._0x80072F78.Services.ForCompany;

public class VehicleService : IVehicleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextData _httpContextData;

    public VehicleService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextData httpContextData)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextData = httpContextData;
    }

    public async Task<CustomResponse<NoContent>> CreateAsync(VehicleCreateDTO vehicleCreateDTO)
    {
        try
        {
            var driver = await _unitOfWork.DriverRepository.AnyAsync(x => x.Id == vehicleCreateDTO.DriverId);
            if (!driver)
            {
                return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(driver));
            }
            await _unitOfWork.BeginTransactionAsync();
            var mappedVehicle = _mapper.Map<Vehicle>(vehicleCreateDTO);
            await _unitOfWork.VehicleRepository.CreateAsync(mappedVehicle);
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
            var vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(id);
            if (vehicle == null)
            {
                return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(vehicle));
            }
            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.VehicleRepository.DeleteAsync(vehicle);
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

    public async Task<CustomResponse<List<VehicleDTO>>> GetAllAsync(bool relational)
    {
        var vehicleList = await _unitOfWork.VehicleRepository.GetAllVehicleAsync(relational);

        return CustomResponse<List<VehicleDTO>>.Success(StatusCodes.Status200OK, _mapper.Map<List<VehicleDTO>>(vehicleList));
    }

    public async Task<CustomResponse<VehicleDTO>> GetByIdAsync(Guid id)
    {
        var vehicle = await _unitOfWork.VehicleRepository.GetVehicleByIdAsync(id);
        if (vehicle == null)
            return CustomResponse<VehicleDTO>.Fail(StatusCodes.Status404NotFound, nameof(vehicle));

        var vehicleDTO = _mapper.Map<VehicleDTO>(vehicle);

        return CustomResponse<VehicleDTO>.Success(StatusCodes.Status200OK, vehicleDTO);
    }

    public async Task<CustomResponse<List<VehicleDTO>>> MyVehiclesAsync()
    {
        var vehicleList = await _unitOfWork.VehicleRepository.GetAllByFilterAsync(x => x.UserId == Guid.Parse(_httpContextData.UserId)
                                                    , null, $"{nameof(Core.Entities.ForCompany.Vehicle.Driver)}");
        if (vehicleList == null)
            return CustomResponse<List<VehicleDTO>>.Fail(StatusCodes.Status404NotFound, nameof(Core.Entities.ForCompany.Vehicle));

        var result = _mapper.Map<List<VehicleDTO>>(vehicleList);

        return CustomResponse<List<VehicleDTO>>.Success(StatusCodes.Status200OK, result);
    }

    public async Task<CustomResponse<NoContent>> UpdateAsync(VehicleUpdateDTO vehicleUpdateDTO)
    {
        try
        {
            var vehicle = await _unitOfWork.VehicleRepository.AnyAsync(x => x.Id == vehicleUpdateDTO.Id);
            if (!vehicle)
            {
                return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(vehicle));
            }
            var driver = await _unitOfWork.DriverRepository.AnyAsync(x => x.Id == vehicleUpdateDTO.DriverId);
            if (!driver)
            {
                return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(driver));
            }
            await _unitOfWork.BeginTransactionAsync();
            var result = _mapper.Map<Vehicle>(vehicleUpdateDTO);
            await _unitOfWork.VehicleRepository.UpdateAsync(result);
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

    public async Task<CustomResponse<List<VehicleDTO>>> AvailableVehiclesAsync()
    {
        List<Vehicle> avaiableVehicles = (await _unitOfWork.VehicleRepository.GetAllByFilterAsync(x => x.VehicleStatus == VehicleStatus.Available && x.UserId == Guid.Parse(_httpContextData.UserId), null, nameof(Vehicle.Driver))).ToList();
        List<VehicleDTO> avaiableVehiclesDTO = _mapper.Map<List<VehicleDTO>>(avaiableVehicles);

        return CustomResponse<List<VehicleDTO>>.Success(StatusCodes.Status200OK, avaiableVehiclesDTO);
    }
}