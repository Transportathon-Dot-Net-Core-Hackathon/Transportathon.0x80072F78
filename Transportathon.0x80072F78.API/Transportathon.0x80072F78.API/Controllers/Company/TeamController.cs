using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Transportathon._0x80072F78.Core.DTOs.ForCompany;
using Transportathon._0x80072F78.Services.ForCompany;
using Transportathon._0x80072F78.Shared.Models;

namespace Transportathon._0x80072F78.API.Controllers.Company
{
    public class TeamController : CustomBaseController
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }
        [HttpPost]
        public async Task<ActionResult<CustomResponse<NoContent>>> Create(TeamCreateDTO teamCreateDTO )
        {
            return CreateActionResultInstance(await _teamService.CreateAsync(teamCreateDTO));

        }
        [HttpPut]
        public async Task<ActionResult<CustomResponse<NoContent>>> Update(TeamUpdateDTO teamUpdateDTO)
        {
            return CreateActionResultInstance(await _teamService.UpdateAsync(teamUpdateDTO));

        }
        [HttpDelete]
        public async Task<ActionResult<CustomResponse<NoContent>>> Delete(Guid id)
        {
            return CreateActionResultInstance(await _teamService.DeleteAsync(id));

        }
        [HttpGet]
        public async Task<ActionResult<CustomResponse<NoContent>>> GetAll()
        {
            return CreateActionResultInstance(await _teamService.GetAllAsync());

        }
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<CustomResponse<NoContent>>> GetTeam(Guid id)
        {
            return CreateActionResultInstance(await _teamService.GetByIdAsync(id));

        }
    }
}
