using Microsoft.AspNetCore.Mvc;
using Transportathon._0x80072F78.Core.DTOs;
using Transportathon._0x80072F78.Services;
using Transportathon._0x80072F78.Shared.Models;

namespace Transportathon._0x80072F78.API.Controllers
{
    public class TransportationRequestController : CustomBaseController
    {
        private readonly ITransportationRequestService _transportationRequestService;

        public TransportationRequestController(ITransportationRequestService transportationRequestService)
        {
            _transportationRequestService = transportationRequestService;
        }

        [HttpPost]
        public async Task<ActionResult<CustomResponse<NoContent>>> Create(TransportationRequestCreateDTO transportationRequestCreateDTO)
        {
            return CreateActionResultInstance(await _transportationRequestService.CreateAsync(transportationRequestCreateDTO));
        }

        [HttpPut]
        public async Task<ActionResult<CustomResponse<NoContent>>> Update(TransportationRequestUpdateDTO transportationRequestUpdateDTO)
        {
            return CreateActionResultInstance(await _transportationRequestService.UpdateAsync(transportationRequestUpdateDTO));
        }

        [HttpDelete]
        public async Task<ActionResult<CustomResponse<NoContent>>> Delete(Guid id)
        {
            return CreateActionResultInstance(await _transportationRequestService.DeleteAsync(id));
        }

        [HttpGet]
        public async Task<ActionResult<CustomResponse<NoContent>>> GetAll(bool relational)
        {
            return CreateActionResultInstance(await _transportationRequestService.GetAllAsync(relational));
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<CustomResponse<NoContent>>> GetTransportationRequest(Guid id)
        {
            return CreateActionResultInstance(await _transportationRequestService.GetByIdAsync(id));
        }

        [HttpPut]
        public async Task<ActionResult<CustomResponse<TransportationRequestDTO>>> ChangeTransportationRequestStatus(StatusUpdateDTO statusUpdateDTO)
        {
            return CreateActionResultInstance(await _transportationRequestService.ChangeTransportationRequestStatusAsync(statusUpdateDTO));
        }
    }
}