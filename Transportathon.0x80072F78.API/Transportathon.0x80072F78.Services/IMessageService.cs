using Transportathon._0x80072F78.Core.DTOs;
using Transportathon._0x80072F78.Shared.Models;

namespace Transportathon._0x80072F78.Services;

public interface IMessageService
{
    Task<CustomResponse<List<MyMessagesDTO>>> GetAllAsync();
    Task<CustomResponse<List<MessageDTO>>> GetMessagesByReceiverIdAsync(Guid receiverId);
    Task<CustomResponse<List<MessageDTO>>> CreateAsync(MessageCreateDTO messageCreateDTO);
}