using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transportathon._0x80072F78.Core.DTOs.ForCompany;
using Transportathon._0x80072F78.Shared.Models;

namespace Transportathon._0x80072F78.Services;
[Headers("Authorization: Bearer")]
public interface IRefitAPI
{
    [Delete("" )]
    Task<CustomResponse<NoContent>> DeleteDriver(Guid id);
    [Post("")]
    Task<CustomResponse<NoContent>> CreateDriver(DriverCreateDTO driverCreateDTO);
    [Put("")]
    Task<CustomResponse<NoContent>> UpdateDriver(DriverUpdateDTO driverUpdateDTO );
}