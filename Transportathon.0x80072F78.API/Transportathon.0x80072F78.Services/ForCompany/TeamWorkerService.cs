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

public class TeamWorkerService : ITeamWorkerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextData _httpContextData;

    public TeamWorkerService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextData httpContextData)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextData = httpContextData;
    }

    public async Task<CustomResponse<NoContent>> CreateAsync(TeamWorkerCreateDTO teamWorkerCreateDTO)
    {
        var mappedTeamWorker = _mapper.Map<TeamWorker>(teamWorkerCreateDTO);
        await _unitOfWork.TeamWorkerRepository.CreateAsync(mappedTeamWorker);
        await _unitOfWork.SaveAsync();
        return CustomResponse<NoContent>.Success(StatusCodes.Status200OK);
    }

    public async Task<CustomResponse<NoContent>> DeleteAsync(Guid id)
    {
        var teamWorker = await _unitOfWork.TeamWorkerRepository.GetByIdAsync(id);
        if (teamWorker == null)
        {
            return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(TeamWorker));
        }
        await _unitOfWork.TeamWorkerRepository.DeleteAsync(teamWorker);
        await _unitOfWork.SaveAsync();
        return CustomResponse<NoContent>.Success(StatusCodes.Status200OK);
    }

    public async Task<CustomResponse<List<TeamWorkerDTO>>> GetAllAsync()
    {
        var teamWorkerList = await _unitOfWork.TeamWorkerRepository.GetAllAsync();
        return CustomResponse<List<TeamWorkerDTO>>.Success(StatusCodes.Status200OK, _mapper.Map<List<TeamWorkerDTO>>(teamWorkerList));
    }

    public async Task<CustomResponse<TeamWorkerDTO>> GetByIdAsync(Guid id)
    {
        var teamWorker = await _unitOfWork.TeamWorkerRepository.GetByIdAsync(id);
        if (teamWorker == null)
        {
            return CustomResponse<TeamWorkerDTO>.Fail(StatusCodes.Status404NotFound, nameof(TeamWorker));
        }
        var teamWorkerDTO = _mapper.Map<TeamWorkerDTO>(teamWorker);
        return CustomResponse<TeamWorkerDTO>.Success(StatusCodes.Status200OK, teamWorkerDTO);
    }

    public async Task<CustomResponse<List<TeamWorkerDTO>>> MyTeamWorkersAsync()
    {
        var teamWorkerList = await _unitOfWork.TeamWorkerRepository.GetAllByFilterAsync(x => x.UserId == Guid.Parse(_httpContextData.UserId)
                                                    , null, $"{nameof(Core.Entities.ForCompany.TeamWorker.Team)}");
        if (teamWorkerList == null)
            return CustomResponse<List<TeamWorkerDTO>>.Fail(StatusCodes.Status404NotFound, nameof(Core.Entities.ForCompany.TeamWorker));

        var result = _mapper.Map<List<TeamWorkerDTO>>(teamWorkerList);

        return CustomResponse<List<TeamWorkerDTO>>.Success(StatusCodes.Status200OK, result);
    }

    public async Task<CustomResponse<NoContent>> UpdateAsync(TeamWorkerUpdateDTO teamWorkerUpdateDTO)
    {
        var teamWorker = await _unitOfWork.TeamWorkerRepository.AnyAsync(x => x.Id == teamWorkerUpdateDTO.Id);
        if (!teamWorker)
        {
            return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(teamWorker));
        }
        var result = _mapper.Map<TeamWorker>(teamWorkerUpdateDTO);
        await _unitOfWork.TeamWorkerRepository.UpdateAsync(result);
        await _unitOfWork.SaveAsync();
        return CustomResponse<NoContent>.Success(StatusCodes.Status200OK);
    }
}