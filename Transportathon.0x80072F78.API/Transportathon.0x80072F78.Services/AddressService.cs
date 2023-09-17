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
using Transportathon._0x80072F78.Shared.Interfaces;
using Transportathon._0x80072F78.Shared.Models;

namespace Transportathon._0x80072F78.Services;

public class AddressService : IAddressService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextData _httpContextData;

    public AddressService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextData httpContextData)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextData = httpContextData;
    }

    public async Task<CustomResponse<NoContent>> CreateAsync(AddressCreateDTO addressCreateDTO)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            var mappedAddress = _mapper.Map<Address>(addressCreateDTO);
            mappedAddress.UserId = Guid.Parse(_httpContextData.UserId);
            await _unitOfWork.AddressRepository.CreateAsync(mappedAddress);
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
            var address = await _unitOfWork.AddressRepository.GetByIdAsync(id);
            if (address == null)
            {
                return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(address));
            }
            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.AddressRepository.DeleteAsync(address);
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

    public async Task<CustomResponse<List<AddressDTO>>> GetAllAsync(bool relational)
    {
        var addressList = await _unitOfWork.AddressRepository.GetAllAddressAsync(relational);
        return CustomResponse<List<AddressDTO>>.Success(StatusCodes.Status200OK, _mapper.Map<List<AddressDTO>>(addressList));
    }

    public async Task<CustomResponse<AddressDTO>> GetByIdAsync(Guid id)
    {
        var address = await _unitOfWork.AddressRepository.GetAddressByIdAsync(id);
        if (address == null)
        {
            return CustomResponse<AddressDTO>.Fail(StatusCodes.Status404NotFound, nameof(address));
        }
        var addressDTO = _mapper.Map<AddressDTO>(address);
        return CustomResponse<AddressDTO>.Success(StatusCodes.Status200OK, addressDTO);
    }

    public async Task<CustomResponse<List<AddressDTO>>> MyAddressesAsync()
    {
        var addressList = await _unitOfWork.AddressRepository.GetAllByFilterAsync(x => x.UserId == Guid.Parse(_httpContextData.UserId)
                                                    , null, $"");
        if (addressList == null)
            return CustomResponse<List<AddressDTO>>.Fail(StatusCodes.Status404NotFound, nameof(Core.Entities.Address));

        var result = _mapper.Map<List<AddressDTO>>(addressList);

        return CustomResponse<List<AddressDTO>>.Success(StatusCodes.Status200OK, result);
    }

    public async Task<CustomResponse<NoContent>> UpdateAsync(AddressUpdateDTO addressUpdateDTO)
    {
        try
        {
            var address = await _unitOfWork.AddressRepository.AnyAsync(x => x.Id == addressUpdateDTO.Id);
            if (!address)
            {
                return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(address));
            }
            await _unitOfWork.BeginTransactionAsync();
            var result = _mapper.Map<Address>(addressUpdateDTO);
            await _unitOfWork.AddressRepository.UpdateAsync(result);
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