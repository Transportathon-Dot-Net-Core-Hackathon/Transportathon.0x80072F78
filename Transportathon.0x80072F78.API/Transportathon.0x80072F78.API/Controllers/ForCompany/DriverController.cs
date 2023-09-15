using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using Transportathon._0x80072F78.Core.DTOs;
using Transportathon._0x80072F78.Core.DTOs.ForCompany;
using Transportathon._0x80072F78.Services.ForCompany;
using Transportathon._0x80072F78.Shared.Models;

namespace Transportathon._0x80072F78.API.Controllers.Company
{
    public class DriverController : CustomBaseController
    {
        private readonly IDriverService _driverService;

        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }
        [HttpPost]
        public async Task<ActionResult<CustomResponse<NoContent>>> Create(DriverCreateDTO driverCreateDTO )
        {
            return CreateActionResultInstance(await _driverService.CreateAsync(driverCreateDTO));

        }
        [HttpPut]
        public async Task<ActionResult<CustomResponse<NoContent>>> Update(DriverUpdateDTO driverUpdateDTO )
        {
            return CreateActionResultInstance(await _driverService.UpdateAsync(driverUpdateDTO));

        }
        [HttpDelete]
        public async Task<ActionResult<CustomResponse<NoContent>>> Delete(Guid id)
        {
            return CreateActionResultInstance(await _driverService.DeleteAsync(id));

        }
        [HttpGet]
        public async Task<ActionResult<CustomResponse<NoContent>>> GetAll()
        {
            return CreateActionResultInstance(await _driverService.GetAllAsync());

        }
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<CustomResponse<NoContent>>> GetDriver(Guid id)
        {
            return CreateActionResultInstance(await _driverService.GetByIdAsync(id));

        }
    }
}
