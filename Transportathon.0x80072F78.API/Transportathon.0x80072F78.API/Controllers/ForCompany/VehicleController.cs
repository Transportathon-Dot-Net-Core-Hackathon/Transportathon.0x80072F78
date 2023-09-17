using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Transportathon._0x80072F78.Core.DTOs;
using Transportathon._0x80072F78.Core.DTOs.ForCompany;
using Transportathon._0x80072F78.Services.ForCompany;
using Transportathon._0x80072F78.Shared.Models;

namespace Transportathon._0x80072F78.API.Controllers.Company
{
    public class VehicleController : CustomBaseController
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpPost]
        public async Task<ActionResult<CustomResponse<NoContent>>> Create(VehicleCreateDTO vehicleCreateDTO)
        {
            return CreateActionResultInstance(await _vehicleService.CreateAsync(vehicleCreateDTO));
        }

        [HttpPut]
        public async Task<ActionResult<CustomResponse<NoContent>>> Update(VehicleUpdateDTO vehicleUpdateDTO)
        {
            return CreateActionResultInstance(await _vehicleService.UpdateAsync(vehicleUpdateDTO));

        }

        [HttpDelete]
        public async Task<ActionResult<CustomResponse<NoContent>>> Delete(Guid id)
        {
            return CreateActionResultInstance(await _vehicleService.DeleteAsync(id));

        }

        [HttpGet]
        public async Task<ActionResult<CustomResponse<NoContent>>> GetAll(bool relational)
        {
            return CreateActionResultInstance(await _vehicleService.GetAllAsync(relational));

        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<CustomResponse<NoContent>>> GetVehicle(Guid id)
        {
            return CreateActionResultInstance(await _vehicleService.GetByIdAsync(id));

        }

        [HttpGet]
        public async Task<ActionResult<CustomResponse<List<VehicleDTO>>>> AvailableVehicles()
        {
            return CreateActionResultInstance(await _vehicleService.AvailableVehiclesAsync());

        }
    }
}
