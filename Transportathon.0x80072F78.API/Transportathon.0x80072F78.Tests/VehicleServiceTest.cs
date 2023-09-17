using AutoMapper;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Transportathon._0x80072F78.Core.DTOs.ForCompany;
using Transportathon._0x80072F78.Core.Entities.ForCompany;
using Transportathon._0x80072F78.Core.Mapping;
using Transportathon._0x80072F78.Core.Repository;
using Transportathon._0x80072F78.Services.ForCompany;
using Transportathon._0x80072F78.Shared.Interfaces;

namespace Transportathon._0x80072F78.Tests;

public class VehicleServiceTest
{
    private readonly IMapper _mapper;
    private readonly VehicleService _vehicleService;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IHttpContextData> _httpContextData;


    public VehicleServiceTest()
    {
        _unitOfWork = new();
        _httpContextData = new();

        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        _mapper = mapperConfiguration.CreateMapper();
        _vehicleService = new VehicleService(_unitOfWork.Object, _mapper, _httpContextData.Object);

    }

    [Fact]
    public async Task CreateAsync_ValidInput_ReturnsSuccess()
    {
        var vehicle = new Vehicle();
        _unitOfWork.Setup(x => x.VehicleRepository.CreateAsync(vehicle)).Returns(Task.CompletedTask);
        _unitOfWork.Setup(uow => uow.DriverRepository.AnyAsync(It.IsAny<Expression<Func<Driver, bool>>>())).ReturnsAsync(true);
        var vehicleCreateDTO = _mapper.Map<VehicleCreateDTO>(vehicle);
        var result = await _vehicleService.CreateAsync(vehicleCreateDTO);
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
    }
    [Fact]
    public async Task DeleteAsync_ValidInput_ReturnsSuccess()
    {
        var guid = Guid.NewGuid();
        _unitOfWork.Setup(x => x.VehicleRepository.DeleteAsync(new Vehicle())).Returns(Task.CompletedTask);
        _unitOfWork.Setup(x => x.VehicleRepository.GetByIdAsync(guid)).ReturnsAsync(new Vehicle());
        var result = await _vehicleService.DeleteAsync(guid);
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
    }
    [Fact]
    public async Task UpdateAsync_ValidInput_ReturnsSuccess()
    {
        var vehicle = new Vehicle();
        _unitOfWork.Setup(x => x.VehicleRepository.UpdateAsync(vehicle)).Returns(Task.CompletedTask);
        _unitOfWork.Setup(uow => uow.VehicleRepository.AnyAsync(It.IsAny<Expression<Func<Vehicle, bool>>>())).ReturnsAsync(true);
        _unitOfWork.Setup(uow => uow.DriverRepository.AnyAsync(It.IsAny<Expression<Func<Driver, bool>>>())).ReturnsAsync(true);
        var vehicleUpdateDTO = _mapper.Map<VehicleUpdateDTO>(vehicle);
        var result = await _vehicleService.UpdateAsync(vehicleUpdateDTO);
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
    }
}