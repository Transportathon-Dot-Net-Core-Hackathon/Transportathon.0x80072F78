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

public class TeamServiceTest
{
    private readonly IMapper _mapper;
    private readonly TeamService _teamService;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IHttpContextData> _httpContextData;


    public TeamServiceTest()
    {
        _unitOfWork = new();
        _httpContextData = new();

        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        _mapper = mapperConfiguration.CreateMapper();
        _teamService = new TeamService(_unitOfWork.Object, _mapper, _httpContextData.Object);

    }

    [Fact]
    public async Task CreateAsync_ValidInput_ReturnsSuccess()
    {
        var team = new Team();
        _unitOfWork.Setup(x => x.TeamRepository.CreateAsync(team)).Returns(Task.CompletedTask);
        _unitOfWork.Setup(uow => uow.CompanyRepository.AnyAsync(It.IsAny<Expression<Func<Company, bool>>>())).ReturnsAsync(true);
        var teamCreateDTO = _mapper.Map<TeamCreateDTO>(team);
        var result = await _teamService.CreateAsync(teamCreateDTO);
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
    }
    [Fact]
    public async Task DeleteAsync_ValidInput_ReturnsSuccess()
    {
        var guid = Guid.NewGuid();
        _unitOfWork.Setup(x => x.TeamRepository.DeleteAsync(new Team())).Returns(Task.CompletedTask);
        _unitOfWork.Setup(x => x.TeamRepository.GetByIdAsync(guid)).ReturnsAsync(new Team());
        var result = await _teamService.DeleteAsync(guid);
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
    }
    [Fact]
    public async Task UpdateAsync_ValidInput_ReturnsSuccess()
    {
        var team = new Team();
        _unitOfWork.Setup(x => x.TeamRepository.UpdateAsync(team)).Returns(Task.CompletedTask);
        _unitOfWork.Setup(uow => uow.TeamRepository.AnyAsync(It.IsAny<Expression<Func<Team, bool>>>())).ReturnsAsync(true);
        _unitOfWork.Setup(uow => uow.CompanyRepository.AnyAsync(It.IsAny<Expression<Func<Company, bool>>>())).ReturnsAsync(true);
        var teamUpdateDTO = _mapper.Map<TeamUpdateDTO>(team);
        var result = await _teamService.UpdateAsync(teamUpdateDTO);
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
    }
}