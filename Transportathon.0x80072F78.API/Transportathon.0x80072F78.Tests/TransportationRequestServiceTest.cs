using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportathon._0x80072F78.Core.DTOs.ForCompany;
using Transportathon._0x80072F78.Core.Entities;
using Transportathon._0x80072F78.Core.Entities.ForCompany;
using Transportathon._0x80072F78.Core.Entities.Identity;
using Transportathon._0x80072F78.Core.Mapping;
using Transportathon._0x80072F78.Core.Repository;
using Transportathon._0x80072F78.Services.ForCompany;
using Transportathon._0x80072F78.Shared.Interfaces;

namespace Transportathon._0x80072F78.Tests;

/*public class TransportationRequestServiceTest
{
    private readonly IMapper _mapper;
    private readonly TransportationRequest _transportationRequest;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IHttpContextData> _httpContextData;
    private readonly Mock<UserManager<AspNetUser>> _userManager;

    public TransportationRequestServiceTest()
    {
        _unitOfWork = new();
        _httpContextData = new();
        _userManager = new();
        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        _mapper = mapperConfiguration.CreateMapper();
        _transportationRequest = new TransportationRequest(_unitOfWork.Object, _mapper, _httpContextData.Object , _userManager.Object);

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
}*/