using AutoMapper;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Transportathon._0x80072F78.Core.DTOs;
using Transportathon._0x80072F78.Core.DTOs.ForCompany;
using Transportathon._0x80072F78.Core.Entities;
using Transportathon._0x80072F78.Core.Entities.ForCompany;
using Transportathon._0x80072F78.Core.Mapping;
using Transportathon._0x80072F78.Core.Repository;
using Transportathon._0x80072F78.Services;
using Transportathon._0x80072F78.Services.ForCompany;
using Transportathon._0x80072F78.Shared.Interfaces;

namespace Transportathon._0x80072F78.Tests;

public class AddressServiceTest
{
    private readonly IMapper _mapper;
    private readonly AddressService _addressService;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IHttpContextData> _httpContextData;


    public AddressServiceTest()
    {
        _unitOfWork = new();
        _httpContextData = new();

        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        _mapper = mapperConfiguration.CreateMapper();
        _addressService = new AddressService(_unitOfWork.Object, _mapper, _httpContextData.Object);

    }

    [Fact]
    public async Task CreateAsync_ValidInput_ReturnsSuccess()
    {
        var address = new Address();
        _unitOfWork.Setup(x => x.AddressRepository.CreateAsync(address)).Returns(Task.CompletedTask);
        var addressCreateDTO = _mapper.Map<AddressCreateDTO>(address);
        var result = await _addressService.CreateAsync(addressCreateDTO);
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
    }
    [Fact]
    public async Task DeleteAsync_ValidInput_ReturnsSuccess()
    {
        var guid = Guid.NewGuid();
        _unitOfWork.Setup(x => x.AddressRepository.DeleteAsync(new Address())).Returns(Task.CompletedTask);
        _unitOfWork.Setup(x => x.AddressRepository.GetByIdAsync(guid)).ReturnsAsync(new Address());
        var result = await _addressService.DeleteAsync(guid);
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
    }
    [Fact]
    public async Task UpdateAsync_ValidInput_ReturnsSuccess()
    {
        var address = new Address();
        _unitOfWork.Setup(x => x.AddressRepository.UpdateAsync(address)).Returns(Task.CompletedTask);
        _unitOfWork.Setup(uow => uow.AddressRepository.AnyAsync(It.IsAny<Expression<Func<Address, bool>>>())).ReturnsAsync(true);
        var addressUpdateDTO = _mapper.Map<AddressUpdateDTO>(address);
        var result = await _addressService.UpdateAsync(addressUpdateDTO);
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
    }
}