﻿using Transportathon._0x80072F78.Core.Entities;
using Transportathon._0x80072F78.Shared.Interfaces;

namespace Transportathon._0x80072F78.Core.Repository;

public interface ITransportationRequestRepository : IAsyncRepository<TransportationRequest>
{
    Task<TransportationRequest> GetTransportationRequestByIdAsync(Guid id);
}