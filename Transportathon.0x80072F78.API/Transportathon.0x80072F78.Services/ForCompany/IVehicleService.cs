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

public interface IVehicleService
{
    Task<CustomResponse<List<VehicleDTO>>> GetAllAsync();
    Task<CustomResponse<NoContent>> DeleteAsync(Guid id);
    Task<CustomResponse<NoContent>> UpdateAsync(VehicleUpdateDTO vehicleUpdateDTO );
    Task<CustomResponse<NoContent>> CreateAsync(VehicleCreateDTO vehicleCreateDTO );
    Task<CustomResponse<VehicleDTO>> GetByIdAsync(Guid id);
    Task<CustomResponse<List<VehicleDTO>>> MyVehiclesAsync();
}