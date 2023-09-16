using Transportathon._0x80072F78.Core.DTOs;
using Transportathon._0x80072F78.Shared.Models;

namespace Transportathon._0x80072F78.Services;

public interface ITransportationRequestService
{
    Task<CustomResponse<List<TransportationRequestDTO>>> GetAllAsync(bool relational);
    Task<CustomResponse<NoContent>> DeleteAsync(Guid id);
    Task<CustomResponse<NoContent>> UpdateAsync(TransportationRequestUpdateDTO transportationRequestUpdateDTO );
    Task<CustomResponse<NoContent>> CreateAsync(TransportationRequestCreateDTO transportationRequestCreateDTO );
    Task<CustomResponse<TransportationRequestDTO>> GetByIdAsync(Guid id);
    Task<CustomResponse<List<TransportationRequestDTO>>> MyTransportationRequestsAsync();
}