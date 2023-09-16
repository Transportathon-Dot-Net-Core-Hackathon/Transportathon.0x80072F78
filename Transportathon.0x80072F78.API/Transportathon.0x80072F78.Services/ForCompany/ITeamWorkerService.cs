using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportathon._0x80072F78.Core.DTOs;
using Transportathon._0x80072F78.Core.DTOs.Company;
using Transportathon._0x80072F78.Core.DTOs.ForCompany;
using Transportathon._0x80072F78.Shared.Models;

namespace Transportathon._0x80072F78.Services.ForCompany;

public interface ITeamWorkerService
{
    Task<CustomResponse<List<TeamWorkerDTO>>> GetAllAsync();
    Task<CustomResponse<NoContent>> DeleteAsync(Guid id);
    Task<CustomResponse<NoContent>> UpdateAsync(TeamWorkerUpdateDTO teamWorkerUpdateDTO);
    Task<CustomResponse<NoContent>> CreateAsync(TeamWorkerCreateDTO teamWorkerCreateDTO);
    Task<CustomResponse<TeamWorkerDTO>> GetByIdAsync(Guid id);
}