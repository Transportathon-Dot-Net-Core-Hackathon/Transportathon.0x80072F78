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

public interface IDriverService
{
    Task<CustomResponse<List<DriverDTO>>> GetAllAsync(bool relational);
    Task<CustomResponse<NoContent>> DeleteAsync(Guid id);
    Task<CustomResponse<NoContent>> UpdateAsync(DriverUpdateDTO driverUpdateDTO );
    Task<CustomResponse<NoContent>> CreateAsync(DriverCreateDTO driverCreateDTO );
    Task<CustomResponse<DriverDTO>> GetByIdAsync(Guid id);
    Task<CustomResponse<List<DriverDTO>>> MyDriversAsync();
}