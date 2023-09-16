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

public class CommentService : ICommentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextData _httpContextData;

    public CommentService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextData httpContextData)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextData = httpContextData;
    }

    public async Task<CustomResponse<NoContent>> CreateAsync(CommentCreateDTO commentCreateDTO)
    {
        var offer = await _unitOfWork.OfferRepository.AnyAsync(x=>x.Id == commentCreateDTO.OfferId);
        if (!offer)
        {
            return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(offer));
        }
        var company = await _unitOfWork.CompanyRepository.AnyAsync(x => x.Id == commentCreateDTO.CompanyId);
        if (!company)
        {
            return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(company));
        }
        var mappedComment = _mapper.Map<Comment>(commentCreateDTO);
        await _unitOfWork.CommentRepository.CreateAsync(mappedComment);
        await _unitOfWork.SaveAsync();
        return CustomResponse<NoContent>.Success(StatusCodes.Status200OK);
    }

    public async Task<CustomResponse<NoContent>> DeleteAsync(Guid id)
    {
        var comment = await _unitOfWork.CommentRepository.GetByIdAsync(id);
        if (comment == null)
        {
            return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(comment));
        }
        await _unitOfWork.CommentRepository.DeleteAsync(comment);
        await _unitOfWork.SaveAsync();
        return CustomResponse<NoContent>.Success(StatusCodes.Status200OK);
    }

    public async Task<CustomResponse<List<CommentDTO>>> GetAllAsync()
    {
        var commentList = await _unitOfWork.CommentRepository.GetAllAsync();
        return CustomResponse<List<CommentDTO>>.Success(StatusCodes.Status200OK, _mapper.Map<List<CommentDTO>>(commentList));
    }

    public async Task<CustomResponse<CommentDTO>> GetByIdAsync(Guid id)
    {
        var comment = await _unitOfWork.CommentRepository.GetByIdAsync(id);
        if (comment == null)
        {
            return CustomResponse<CommentDTO>.Fail(StatusCodes.Status404NotFound, nameof(comment));
        }
        var commentDTO = _mapper.Map<CommentDTO>(comment);
        return CustomResponse<CommentDTO>.Success(StatusCodes.Status200OK, commentDTO);
    }

    public async Task<CustomResponse<List<CommentDTO>>> MyCommentsAsync()
    {
        var commentList = await _unitOfWork.CommentRepository.GetAllByFilterAsync(x => x.UserId == Guid.Parse(_httpContextData.UserId)
                                                    , null, $"");
        if (commentList == null)
            return CustomResponse<List<CommentDTO>>.Fail(StatusCodes.Status404NotFound, nameof(Core.Entities.ForCompany.Comment));

        var result = _mapper.Map<List<CommentDTO>>(commentList);

        return CustomResponse<List<CommentDTO>>.Success(StatusCodes.Status200OK, result);
    }

    public async Task<CustomResponse<NoContent>> UpdateAsync(CommentUpdateDTO commentUpdateDTO)
    {
        var comment = await _unitOfWork.CommentRepository.AnyAsync(x => x.Id == commentUpdateDTO.Id);
        if (!comment)
        {
            return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(comment));
        }
        var offer = await _unitOfWork.OfferRepository.AnyAsync(x => x.Id == commentUpdateDTO.OfferId);
        if (!offer)
        {
            return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(offer));
        }
        var company = await _unitOfWork.CompanyRepository.AnyAsync(x => x.Id == commentUpdateDTO.CompanyId);
        if (!company)
        {
            return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(company));
        }
        var result = _mapper.Map<Comment>(commentUpdateDTO);
        await _unitOfWork.CommentRepository.UpdateAsync(result);
        await _unitOfWork.SaveAsync();
        return CustomResponse<NoContent>.Success(StatusCodes.Status200OK);
    }
}