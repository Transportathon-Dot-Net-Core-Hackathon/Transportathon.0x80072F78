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

    public class CommentServiceTests
    {
        private readonly IMapper _mapper;
        private readonly CommentService _commentService;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IHttpContextData> _httpContextData;


        public CommentServiceTests()
        {
            _unitOfWork = new();
            _httpContextData = new();

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfiguration.CreateMapper();
            _commentService = new CommentService(_unitOfWork.Object, _mapper, _httpContextData.Object);

        }

        [Fact]
        public async Task CreateAsync_ValidInput_ReturnsSuccess()
        {
            var comment = new Comment
            {
                CompanyId = Guid.NewGuid(),
                OfferId = Guid.NewGuid(),
                Score = 1,
                Text = "Test",
                Date = DateTime.Now,
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid()

            };
            _unitOfWork.Setup(x => x.CommentRepository.CreateAsync(comment)).Returns(Task.CompletedTask);
            _unitOfWork.Setup(uow => uow.OfferRepository.AnyAsync(It.IsAny<Expression<Func<Offer, bool>>>())).ReturnsAsync(true);
            _unitOfWork.Setup(uow => uow.CompanyRepository.AnyAsync(It.IsAny<Expression<Func<Company, bool>>>())).ReturnsAsync(true);
            var createCommentDTO = _mapper.Map<CommentCreateDTO>(comment);
            var result = await _commentService.CreateAsync(createCommentDTO);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }
        [Fact]
        public async Task DeleteAsync_ValidInput_ReturnsSuccess()
        {
            var guid = Guid.NewGuid();
            _unitOfWork.Setup(x => x.CommentRepository.DeleteAsync(new Comment())).Returns(Task.CompletedTask);
            _unitOfWork.Setup(x => x.CommentRepository.GetByIdAsync(guid)).ReturnsAsync(new Comment());
            var result = await _commentService.DeleteAsync(guid);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }
        [Fact]
        public async Task UpdateAsync_ValidInput_ReturnsSuccess()
        {
            var comment = new Comment
            {
                CompanyId = Guid.NewGuid(),
                OfferId = Guid.NewGuid(),
                Score = 1,
                Text = "Test",
                Date = DateTime.Now,
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid()

            };
            _unitOfWork.Setup(x => x.CommentRepository.UpdateAsync(comment)).Returns(Task.CompletedTask);
            _unitOfWork.Setup(uow => uow.CommentRepository.AnyAsync(It.IsAny<Expression<Func<Comment, bool>>>())).ReturnsAsync(true);
            _unitOfWork.Setup(uow => uow.OfferRepository.AnyAsync(It.IsAny<Expression<Func<Offer, bool>>>())).ReturnsAsync(true);
            _unitOfWork.Setup(uow => uow.CompanyRepository.AnyAsync(It.IsAny<Expression<Func<Company, bool>>>())).ReturnsAsync(true);
            var commentUpdateDTO = _mapper.Map<CommentUpdateDTO>(comment);
            var result = await _commentService.UpdateAsync(commentUpdateDTO);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }
    }