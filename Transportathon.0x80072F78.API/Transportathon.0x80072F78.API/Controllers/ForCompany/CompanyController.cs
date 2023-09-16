using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Transportathon._0x80072F78.Core.DTOs.ForCompany;
using Transportathon._0x80072F78.Services.ForCompany;
using Transportathon._0x80072F78.Shared.Models;

namespace Transportathon._0x80072F78.API.Controllers.Company
{
    public class CompanyController : CustomBaseController
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<CustomResponse<NoContent>>> Create(CompanyCreateDTO companyCreateDTO)
        {
            return CreateActionResultInstance(await _companyService.CreateAsync(companyCreateDTO));
            
        }

        [HttpPut]
        public async Task<ActionResult<CustomResponse<NoContent>>> Update(CompanyUpdateDTO companyUpdateDTO)
        {
            return CreateActionResultInstance(await _companyService.UpdateAsync(companyUpdateDTO));

        }

        [HttpDelete]
        public async Task<ActionResult<CustomResponse<NoContent>>> Delete(Guid id)
        {
            return CreateActionResultInstance(await _companyService.DeleteAsync(id));

        }

        [HttpGet]
        public async Task<ActionResult<CustomResponse<NoContent>>> GetAll()
        {
            return CreateActionResultInstance(await _companyService.GetAllAsync());

        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<CustomResponse<NoContent>>> GetCompany(Guid id)
        {
            return CreateActionResultInstance(await _companyService.GetByIdAsync(id));

        }
    }
}
