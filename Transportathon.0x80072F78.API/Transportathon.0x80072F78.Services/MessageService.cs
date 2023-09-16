using Microsoft.AspNetCore.Http;
using Transportathon._0x80072F78.Core.Repository;
using Transportathon._0x80072F78.Shared.Models;

namespace Transportathon._0x80072F78.Services;

public class MessageService : IMessageService
{
    private readonly IUnitOfWork _unitOfWork;

    public MessageService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CustomResponse<NoContent>> Create(string message)
    {






        return CustomResponse<NoContent>.Success(StatusCodes.Status200OK);
    }
}