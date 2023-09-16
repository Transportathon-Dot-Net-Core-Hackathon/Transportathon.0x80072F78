using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportathon._0x80072F78.Core.DTOs;
using Transportathon._0x80072F78.Core.DTOs.Company;
using Transportathon._0x80072F78.Core.DTOs.ForCompany;
using Transportathon._0x80072F78.Shared.Models;

namespace Transportathon._0x80072F78.Services;

public interface IAddressService
{
    Task<CustomResponse<List<AddressDTO>>> GetAllAsync(bool relational);
    Task<CustomResponse<NoContent>> DeleteAsync(Guid id);
    Task<CustomResponse<NoContent>> UpdateAsync(AddressUpdateDTO addressUpdateDTO );
    Task<CustomResponse<NoContent>> CreateAsync(AddressCreateDTO addressCreateDTO );
    Task<CustomResponse<AddressDTO>> GetByIdAsync(Guid id);
    Task<CustomResponse<List<AddressDTO>>> MyAddressesAsync();
}