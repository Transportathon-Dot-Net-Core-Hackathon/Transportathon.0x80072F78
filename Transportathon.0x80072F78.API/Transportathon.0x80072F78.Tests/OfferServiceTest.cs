using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
using Transportathon._0x80072F78.Core.Entities.Offer;
using Transportathon._0x80072F78.Core.Mapping;
using Transportathon._0x80072F78.Core.Repository;
using Transportathon._0x80072F78.Services.ForCompany;
using Transportathon._0x80072F78.Services.Offer;
using Transportathon._0x80072F78.Shared.Interfaces;

namespace Transportathon._0x80072F78.Tests;

public class OfferServiceTest
{
    private readonly IMapper _mapper;
    private readonly OfferService _offerService;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IHttpContextData> _httpContextData;


    public OfferServiceTest()
    {
        _unitOfWork = new();
        _httpContextData = new();

        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        _mapper = mapperConfiguration.CreateMapper();
        _offerService = new OfferService(_unitOfWork.Object, _mapper, _httpContextData.Object);

    }

    [Fact]
    public async Task CreateAsync_ValidInput_ReturnsSuccess()
    {
        var offer = new Offer();
        _unitOfWork.Setup(x => x.OfferRepository.CreateAsync(offer)).Returns(Task.CompletedTask);
        _unitOfWork.Setup(uow => uow.TransportationRequestRepository.AnyAsync(It.IsAny<Expression<Func<TransportationRequest, bool>>>())).ReturnsAsync(true);
        var offerCreateDTO = _mapper.Map<OfferCreateDTO>(offer);
        var result = await _offerService.CreateAsync(offerCreateDTO);
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
    }
    [Fact]
    public async Task DeleteAsync_ValidInput_ReturnsSuccess()
    {
        var guid = Guid.NewGuid();
        _unitOfWork.Setup(x => x.OfferRepository.DeleteAsync(new Offer())).Returns(Task.CompletedTask);
        _unitOfWork.Setup(x => x.OfferRepository.GetByIdAsync(guid)).ReturnsAsync(new Offer());
        var result = await _offerService.DeleteAsync(guid);
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
    }
    [Fact]
    public async Task UpdateAsync_ValidInput_ReturnsSuccess()
    {
        var offer = new Offer();
        _unitOfWork.Setup(x => x.OfferRepository.UpdateAsync(offer)).Returns(Task.CompletedTask);
        _unitOfWork.Setup(uow => uow.OfferRepository.AnyAsync(It.IsAny<Expression<Func<Offer, bool>>>())).ReturnsAsync(true);
        _unitOfWork.Setup(uow => uow.TransportationRequestRepository.AnyAsync(It.IsAny<Expression<Func<TransportationRequest, bool>>>())).ReturnsAsync(true);
        var offerUpdateDTO = _mapper.Map<OfferUpdateDTO>(offer);
        var result = await _offerService.UpdateAsync(offerUpdateDTO);
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
    }
}