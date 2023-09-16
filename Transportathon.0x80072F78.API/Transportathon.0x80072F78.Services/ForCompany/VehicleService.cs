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
using Transportathon._0x80072F78.Core.Entities.ForCompany;
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
        var driver = await _unitOfWork.DriverRepository.AnyAsync(x => x.Id == vehicleCreateDTO.DriverId);
        if (!driver)
        {
            return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(driver));
        }
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
        var result = _mapper.Map<Vehicle>(vehicleUpdateDTO);
        await _unitOfWork.VehicleRepository.UpdateAsync(result);
        await _unitOfWork.SaveAsync();
        return CustomResponse<NoContent>.Success(StatusCodes.Status200OK);
    }
}