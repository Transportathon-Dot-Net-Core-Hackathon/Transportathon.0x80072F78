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

public class DriverService : IDriverService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextData _httpContextData;

    public DriverService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextData httpContextData)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextData = httpContextData;
    }

    public async Task<CustomResponse<NoContent>> CreateAsync(DriverCreateDTO driverCreateDTO)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            var mappedDriver = _mapper.Map<Driver>(driverCreateDTO);
            await _unitOfWork.DriverRepository.CreateAsync(mappedDriver);
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
            var driver = await _unitOfWork.DriverRepository.GetByIdAsync(id);
            if (driver == null)
            {
                return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(driver));
            }
            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.DriverRepository.DeleteAsync(driver);
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

    public async Task<CustomResponse<List<DriverDTO>>> GetAllAsync(bool relational)
    {
        var driverList = await _unitOfWork.DriverRepository.GetAllDriverAsync(relational);
        return CustomResponse<List<DriverDTO>>.Success(StatusCodes.Status200OK, _mapper.Map<List<DriverDTO>>(driverList));
    }

    public async Task<CustomResponse<DriverDTO>> GetByIdAsync(Guid id)
    {
        var driver = await _unitOfWork.DriverRepository.GetDriverByIdAsync(id);
        if (driver == null)
        {
            return CustomResponse<DriverDTO>.Fail(StatusCodes.Status404NotFound, nameof(driver));
        }
        var driverDTO = _mapper.Map<DriverDTO>(driver);
        return CustomResponse<DriverDTO>.Success(StatusCodes.Status200OK, driverDTO);
    }

    public async Task<CustomResponse<List<DriverDTO>>> MyDriversAsync()
    {
        var driverList = await _unitOfWork.DriverRepository.GetAllByFilterAsync(x => x.UserId == Guid.Parse(_httpContextData.UserId)
                                                    , null, $"");
        if (driverList == null)
            return CustomResponse<List<DriverDTO>>.Fail(StatusCodes.Status404NotFound, nameof(Core.Entities.ForCompany.Driver));

        var result = _mapper.Map<List<DriverDTO>>(driverList);

        return CustomResponse<List<DriverDTO>>.Success(StatusCodes.Status200OK, result);
    }

    public async Task<CustomResponse<NoContent>> UpdateAsync(DriverUpdateDTO driverUpdateDTO)
    {
        try
        {
            var driver = await _unitOfWork.DriverRepository.AnyAsync(x => x.Id == driverUpdateDTO.Id);
            if (!driver)
            {
                return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(driver));
            }
            await _unitOfWork.BeginTransactionAsync();
            var result = _mapper.Map<Driver>(driverUpdateDTO);
            await _unitOfWork.DriverRepository.UpdateAsync(result);
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
}