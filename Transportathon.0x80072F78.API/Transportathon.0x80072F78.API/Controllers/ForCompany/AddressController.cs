using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Transportathon._0x80072F78.Core.DTOs;
using Transportathon._0x80072F78.Core.DTOs.ForCompany;
using Transportathon._0x80072F78.Services;
using Transportathon._0x80072F78.Shared.Models;

namespace Transportathon._0x80072F78.API.Controllers.Company
{
    public class AddressController : CustomBaseController
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }
        [HttpPost]
        public async Task<ActionResult<CustomResponse<NoContent>>> Create(AddressCreateDTO addressCreateDTO )
        {
            return CreateActionResultInstance(await _addressService.CreateAsync(addressCreateDTO));

        }
        [HttpPut]
        public async Task<ActionResult<CustomResponse<NoContent>>> Update(AddressUpdateDTO addressUpdateDTO)
        {
            return CreateActionResultInstance(await _addressService.UpdateAsync(addressUpdateDTO));

        }
        [HttpDelete]
        public async Task<ActionResult<CustomResponse<NoContent>>> Delete(Guid id)
        {
            return CreateActionResultInstance(await _addressService.DeleteAsync(id));

        }
        [HttpGet]
        public async Task<ActionResult<CustomResponse<NoContent>>> GetAll(bool relational)
        {
            return CreateActionResultInstance(await _addressService.GetAllAsync(relational));

        }
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<CustomResponse<NoContent>>> GetAddress(Guid id)
        {
            return CreateActionResultInstance(await _addressService.GetByIdAsync(id));

        }
    }
}
