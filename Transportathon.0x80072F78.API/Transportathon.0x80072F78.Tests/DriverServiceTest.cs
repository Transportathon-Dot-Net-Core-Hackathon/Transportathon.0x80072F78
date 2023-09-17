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
using Transportathon._0x80072F78.Core.Entities.Offer;
using Transportathon._0x80072F78.Core.Mapping;
using Transportathon._0x80072F78.Core.Repository;
using Transportathon._0x80072F78.Services.ForCompany;
using Transportathon._0x80072F78.Shared.Interfaces;

namespace Transportathon._0x80072F78.Tests;

public class DriverServiceTest
{
    private readonly IMapper _mapper;
    private readonly DriverService _driverService;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IHttpContextData> _httpContextData;


    public DriverServiceTest()
    {
        _unitOfWork = new();
        _httpContextData = new();

        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        _mapper = mapperConfiguration.CreateMapper();
        _driverService = new DriverService(_unitOfWork.Object, _mapper, _httpContextData.Object);

    }

    [Fact]
    public async Task CreateAsync_ValidInput_ReturnsSuccess()
    {
        var driver = new Driver();
        _unitOfWork.Setup(x => x.DriverRepository.CreateAsync(driver)).Returns(Task.CompletedTask);
        var driverCreateDTO = _mapper.Map<DriverCreateDTO>(driver);
        var result = await _driverService.CreateAsync(driverCreateDTO);
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
    }
    [Fact]
    public async Task DeleteAsync_ValidInput_ReturnsSuccess()
    {
        var guid = Guid.NewGuid();
        _unitOfWork.Setup(x => x.DriverRepository.DeleteAsync(new Driver())).Returns(Task.CompletedTask);
        _unitOfWork.Setup(x => x.DriverRepository.GetByIdAsync(guid)).ReturnsAsync(new Driver());
        var result = await _driverService.DeleteAsync(guid);
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
    }
    [Fact]
    public async Task UpdateAsync_ValidInput_ReturnsSuccess()
    {
        var driver = new Driver();
        _unitOfWork.Setup(x => x.DriverRepository.UpdateAsync(driver)).Returns(Task.CompletedTask);
        _unitOfWork.Setup(uow => uow.DriverRepository.AnyAsync(It.IsAny<Expression<Func<Driver, bool>>>())).ReturnsAsync(true);
        var driverUpdateDTO = _mapper.Map<DriverUpdateDTO>(driver);
        var result = await _driverService.UpdateAsync(driverUpdateDTO);
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
    }
}