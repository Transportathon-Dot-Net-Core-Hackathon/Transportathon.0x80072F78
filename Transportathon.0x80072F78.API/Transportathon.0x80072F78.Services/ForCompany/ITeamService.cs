using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportathon._0x80072F78.Core.DTOs.Company;
using Transportathon._0x80072F78.Core.DTOs.ForCompany;
using Transportathon._0x80072F78.Shared.Models;

namespace Transportathon._0x80072F78.Services.ForCompany;

public interface ITeamService
{
    Task<CustomResponse<List<TeamDTO>>> GetAllAsync(bool relational);
    Task<CustomResponse<NoContent>> DeleteAsync(Guid id);
    Task<CustomResponse<NoContent>> UpdateAsync(TeamUpdateDTO teamUpdateDTO );
    Task<CustomResponse<NoContent>> CreateAsync(TeamCreateDTO teamCreateDTO );
    Task<CustomResponse<TeamDTO>> GetByIdAsync(Guid id);
}