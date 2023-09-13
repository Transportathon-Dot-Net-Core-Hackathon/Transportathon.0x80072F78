﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportathon._0x80072F78.Core.DTOs.Company;
using Transportathon._0x80072F78.Core.DTOs.ForCompany;
using Transportathon._0x80072F78.Core.Entities.ForCompany;
using Transportathon._0x80072F78.Core.Repository;
using Transportathon._0x80072F78.Shared.Models;

namespace Transportathon._0x80072F78.Services.ForCompany;

public class CommentService : ICommentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CustomResponse<NoContent>> CreateAsync(CommentCreateDTO commentCreateDTO)
    {
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

    public async Task<CustomResponse<NoContent>> UpdateAsync(CommentUpdateDTO commentUpdateDTO)
    {
        var comment = await _unitOfWork.CommentRepository.AnyAsync(x => x.Id == commentUpdateDTO.Id);
        if (!comment)
        {
            return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(comment));
        }
        var result = _mapper.Map<Comment>(commentUpdateDTO);
        await _unitOfWork.CommentRepository.UpdateAsync(result);
        await _unitOfWork.SaveAsync();
        return CustomResponse<NoContent>.Success(StatusCodes.Status200OK);
    }
}