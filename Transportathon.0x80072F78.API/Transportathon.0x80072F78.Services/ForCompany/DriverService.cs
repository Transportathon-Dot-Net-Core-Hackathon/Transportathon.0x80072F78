﻿using AutoMapper;
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

public class DriverService : IDriverService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DriverService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CustomResponse<NoContent>> CreateAsync(DriverCreateDTO driverCreateDTO)
    {
        var mappedDriver = _mapper.Map<Driver>(driverCreateDTO);
        await _unitOfWork.DriverRepository.CreateAsync(mappedDriver);
        await _unitOfWork.SaveAsync();
        return CustomResponse<NoContent>.Success(StatusCodes.Status200OK);
    }

    public async Task<CustomResponse<NoContent>> DeleteAsync(Guid id)
    {
        var driver = await _unitOfWork.DriverRepository.GetByIdAsync(id);
        if (driver == null)
        {
            return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(driver));
        }
        await _unitOfWork.DriverRepository.DeleteAsync(driver);
        await _unitOfWork.SaveAsync();
        return CustomResponse<NoContent>.Success(StatusCodes.Status200OK);
    }

    public async Task<CustomResponse<List<DriverDTO>>> GetAllAsync()
    {
        var driverList = await _unitOfWork.DriverRepository.GetAllAsync();
        return CustomResponse<List<DriverDTO>>.Success(StatusCodes.Status200OK, _mapper.Map<List<DriverDTO>>(driverList));
    }

    public async Task<CustomResponse<DriverDTO>> GetByIdAsync(Guid id)
    {
        var driver = await _unitOfWork.DriverRepository.GetByIdAsync(id);
        if (driver == null)
        {
            return CustomResponse<DriverDTO>.Fail(StatusCodes.Status404NotFound, nameof(driver));
        }
        var driverDTO = _mapper.Map<DriverDTO>(driver);
        return CustomResponse<DriverDTO>.Success(StatusCodes.Status200OK, driverDTO);
    }

    public async Task<CustomResponse<NoContent>> UpdateAsync(DriverUpdateDTO driverUpdateDTO)
    {
        var driver = await _unitOfWork.DriverRepository.AnyAsync(x => x.Id == driverUpdateDTO.Id);
        if (!driver)
        {
            return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(driver));
        }
        var result = _mapper.Map<Driver>(driverUpdateDTO);
        await _unitOfWork.DriverRepository.UpdateAsync(result);
        await _unitOfWork.SaveAsync();
        return CustomResponse<NoContent>.Success(StatusCodes.Status200OK);
    }
}