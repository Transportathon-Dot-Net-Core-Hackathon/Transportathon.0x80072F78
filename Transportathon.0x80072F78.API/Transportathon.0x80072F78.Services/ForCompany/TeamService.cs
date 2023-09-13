using AutoMapper;
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

public class TeamService : ITeamService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TeamService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CustomResponse<NoContent>> CreateAsync(TeamCreateDTO teamCreateDTO)
    {
        var mappedTeam = _mapper.Map<Team>(teamCreateDTO);
        await _unitOfWork.TeamRepository.CreateAsync(mappedTeam);
        await _unitOfWork.SaveAsync();
        return CustomResponse<NoContent>.Success(StatusCodes.Status200OK);
    }

    public async Task<CustomResponse<NoContent>> DeleteAsync(Guid id)
    {
        var team = await _unitOfWork.TeamRepository.GetByIdAsync(id);
        if (team == null)
        {
            return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(team));
        }
        await _unitOfWork.TeamRepository.DeleteAsync(team);
        await _unitOfWork.SaveAsync();
        return CustomResponse<NoContent>.Success(StatusCodes.Status200OK);
    }

    public async Task<CustomResponse<List<TeamDTO>>> GetAllAsync()
    {
        var teamList = await _unitOfWork.TeamRepository.GetAllAsync();
        return CustomResponse<List<TeamDTO>>.Success(StatusCodes.Status200OK, _mapper.Map<List<TeamDTO>>(teamList));
    }

    public async Task<CustomResponse<TeamDTO>> GetByIdAsync(Guid id)
    {
        var team = await _unitOfWork.TeamRepository.GetByIdAsync(id);
        if (team == null)
        {
            return CustomResponse<TeamDTO>.Fail(StatusCodes.Status404NotFound, nameof(team));
        }
        var teamDTO = _mapper.Map<TeamDTO>(team);
        return CustomResponse<TeamDTO>.Success(StatusCodes.Status200OK, teamDTO);
    }

    public async Task<CustomResponse<NoContent>> UpdateAsync(TeamUpdateDTO teamUpdateDTO)
    {
        var team = await _unitOfWork.TeamRepository.AnyAsync(x => x.Id == teamUpdateDTO.Id);
        if (!team)
        {
            return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(team));
        }
        var result = _mapper.Map<Team>(teamUpdateDTO);
        await _unitOfWork.TeamRepository.UpdateAsync(result);
        await _unitOfWork.SaveAsync();
        return CustomResponse<NoContent>.Success(StatusCodes.Status200OK);
    }
}