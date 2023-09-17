using AutoMapper;
using Microsoft.AspNetCore.Http;
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
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var mappedTeamWorker = _mapper.Map<TeamWorker>(teamWorkerCreateDTO);
            mappedTeamWorker.UserId = Guid.Parse(_httpContextData.UserId);

            await _unitOfWork.TeamWorkerRepository.CreateAsync(mappedTeamWorker);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitAsync();

            return CustomResponse<NoContent>.Success(StatusCodes.Status200OK);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            return CustomResponse<NoContent>.Fail(StatusCodes.Status400BadRequest, new List<string> { ex.Message });
        }
    }

    public async Task<CustomResponse<NoContent>> DeleteAsync(Guid id)
    {
        try
        {
            var teamWorker = await _unitOfWork.TeamWorkerRepository.GetByIdAsync(id);
            if (teamWorker == null)
                return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(TeamWorker));

            await _unitOfWork.BeginTransactionAsync();

            await _unitOfWork.TeamWorkerRepository.DeleteAsync(teamWorker);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitAsync();

            return CustomResponse<NoContent>.Success(StatusCodes.Status200OK);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            return CustomResponse<NoContent>.Fail(StatusCodes.Status400BadRequest, new List<string> { ex.Message });
        }
    }

    public async Task<CustomResponse<List<TeamWorkerDTO>>> GetAllAsync(bool relational)
    {
        var teamWorkerList = await _unitOfWork.TeamWorkerRepository.GetAllTeamWorkerAsync(relational);

        return CustomResponse<List<TeamWorkerDTO>>.Success(StatusCodes.Status200OK, _mapper.Map<List<TeamWorkerDTO>>(teamWorkerList));
    }

    public async Task<CustomResponse<TeamWorkerDTO>> GetByIdAsync(Guid id)
    {
        var teamWorker = await _unitOfWork.TeamWorkerRepository.GetTeamWorkerByIdAsync(id);
        if (teamWorker == null)
            return CustomResponse<TeamWorkerDTO>.Fail(StatusCodes.Status404NotFound, nameof(TeamWorker));

        var teamWorkerDTO = _mapper.Map<TeamWorkerDTO>(teamWorker);

        return CustomResponse<TeamWorkerDTO>.Success(StatusCodes.Status200OK, teamWorkerDTO);
    }

    public async Task<CustomResponse<NoContent>> UpdateAsync(TeamWorkerUpdateDTO teamWorkerUpdateDTO)
    {
        try
        {
            var teamWorker = await _unitOfWork.TeamWorkerRepository.AnyAsync(x => x.Id == teamWorkerUpdateDTO.Id);
            if (!teamWorker)
                return CustomResponse<NoContent>.Fail(StatusCodes.Status404NotFound, nameof(teamWorker));

            await _unitOfWork.BeginTransactionAsync();

            var result = _mapper.Map<TeamWorker>(teamWorkerUpdateDTO);
            result.UserId = Guid.Parse(_httpContextData.UserId);

            await _unitOfWork.TeamWorkerRepository.UpdateAsync(result);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitAsync();

            return CustomResponse<NoContent>.Success(StatusCodes.Status200OK);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            return CustomResponse<NoContent>.Fail(StatusCodes.Status400BadRequest, new List<string> { ex.Message });
        }
    }
}