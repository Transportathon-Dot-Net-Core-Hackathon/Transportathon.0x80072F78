using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Transportathon._0x80072F78.Core.DTOs.ForCompany;
using Transportathon._0x80072F78.Services.ForCompany;
using Transportathon._0x80072F78.Shared.Models;

namespace Transportathon._0x80072F78.API.Controllers.Company
{
    public class TeamWorkerController : CustomBaseController
    {
        private readonly ITeamWorkerService _teamWorkerService;

        public TeamWorkerController(ITeamWorkerService teamWorkerService)
        {
            _teamWorkerService = teamWorkerService;
        }
        [HttpPost]
        public async Task<ActionResult<CustomResponse<NoContent>>> Create(TeamWorkerCreateDTO teamWorkerCreateDTO)
        {
            return CreateActionResultInstance(await _teamWorkerService.CreateAsync(teamWorkerCreateDTO));

        }
        [HttpPut]
        public async Task<ActionResult<CustomResponse<NoContent>>> Update(TeamWorkerUpdateDTO teamWorkerUpdateDTO)
        {
            return CreateActionResultInstance(await _teamWorkerService.UpdateAsync(teamWorkerUpdateDTO));

        }
        [HttpDelete]
        public async Task<ActionResult<CustomResponse<NoContent>>> Delete(Guid id)
        {
            return CreateActionResultInstance(await _teamWorkerService.DeleteAsync(id));

        }
        [HttpGet]
        public async Task<ActionResult<CustomResponse<NoContent>>> GetAll(bool relational)
        {
            return CreateActionResultInstance(await _teamWorkerService.GetAllAsync(relational));

        }
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<CustomResponse<NoContent>>> GetTeamWorker(Guid id)
        {
            return CreateActionResultInstance(await _teamWorkerService.GetByIdAsync(id));

        }
    }
}
