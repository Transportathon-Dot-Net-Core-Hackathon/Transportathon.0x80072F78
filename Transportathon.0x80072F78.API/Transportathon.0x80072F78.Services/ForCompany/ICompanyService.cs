using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportathon._0x80072F78.Core.DTOs;
using Transportathon._0x80072F78.Core.DTOs.Company;
using Transportathon._0x80072F78.Core.DTOs.ForCompany;
using Transportathon._0x80072F78.Core.Entities.ForCompany;
using Transportathon._0x80072F78.Shared.Models;

namespace Transportathon._0x80072F78.Services.ForCompany;

public interface ICompanyService
{
    Task<CustomResponse<List<CompanyDTO>>> GetAllAsync(bool relational);
    Task<CustomResponse<NoContent>> DeleteAsync(Guid id);
    Task<CustomResponse<NoContent>> UpdateAsync(CompanyUpdateDTO companyUpdateDTO);
    Task<CustomResponse<NoContent>> CreateAsync(CompanyCreateDTO companyCreateDTO);
    Task<CustomResponse<CompanyDTO>> GetByIdAsync(Guid id);
    Task<CustomResponse<List<CompanyDTO>>> MyCompainesAsync();
}