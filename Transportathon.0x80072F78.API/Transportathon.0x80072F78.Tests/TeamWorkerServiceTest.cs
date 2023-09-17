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

public class TeamWorkerServiceTest
{
    private readonly IMapper _mapper;
    private readonly TeamWorkerService _teamWorkerService;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IHttpContextData> _httpContextData;


    public TeamWorkerServiceTest()
    {
        _unitOfWork = new();
        _httpContextData = new();

        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        _mapper = mapperConfiguration.CreateMapper();
        _teamWorkerService = new TeamWorkerService(_unitOfWork.Object, _mapper, _httpContextData.Object);

    }

    [Fact]
    public async Task CreateAsync_ValidInput_ReturnsSuccess()
    {
        var teamWorker = new TeamWorker();
        _unitOfWork.Setup(x => x.TeamWorkerRepository.CreateAsync(teamWorker)).Returns(Task.CompletedTask);
        var teamWorkerCreateDTO = _mapper.Map<TeamWorkerCreateDTO>(teamWorker);
        var result = await _teamWorkerService.CreateAsync(teamWorkerCreateDTO);
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
    }
    [Fact]
    public async Task DeleteAsync_ValidInput_ReturnsSuccess()
    {
        var guid = Guid.NewGuid();
        _unitOfWork.Setup(x => x.TeamWorkerRepository.DeleteAsync(new TeamWorker())).Returns(Task.CompletedTask);
        _unitOfWork.Setup(x => x.TeamWorkerRepository.GetByIdAsync(guid)).ReturnsAsync(new TeamWorker());
        var result = await _teamWorkerService.DeleteAsync(guid);
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
    }
    [Fact]
    public async Task UpdateAsync_ValidInput_ReturnsSuccess()
    {
        var teamWorker = new TeamWorker();
        _unitOfWork.Setup(x => x.TeamWorkerRepository.UpdateAsync(teamWorker)).Returns(Task.CompletedTask);
        _unitOfWork.Setup(uow => uow.TeamWorkerRepository.AnyAsync(It.IsAny<Expression<Func<TeamWorker, bool>>>())).ReturnsAsync(true);
        var teamWorkerUpdateDTO = _mapper.Map<TeamWorkerUpdateDTO>(teamWorker);
        var result = await _teamWorkerService.UpdateAsync(teamWorkerUpdateDTO);
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
    }
}