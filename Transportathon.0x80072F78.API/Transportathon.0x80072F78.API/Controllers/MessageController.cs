using Microsoft.AspNetCore.Mvc;
using Transportathon._0x80072F78.Core.DTOs;
using Transportathon._0x80072F78.Services;
using Transportathon._0x80072F78.Shared.Models;

namespace Transportathon._0x80072F78.API.Controllers;

public class MessageController : CustomBaseController
{
    private readonly IMessageService _messageService;

    public MessageController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    [HttpGet]
    public async Task<ActionResult<CustomResponse<List<MyMessagesDTO>>>> GetAll()
    {
        return CreateActionResultInstance(await _messageService.GetAllAsync());
    }

    [HttpGet("{receiverId:Guid}")]
    public async Task<ActionResult<CustomResponse<List<MessageDTO>>>> GetMessagesByReceiverId(Guid receiverId)
    {
        return CreateActionResultInstance(await _messageService.GetMessagesByReceiverIdAsync(receiverId));
    }

    [HttpPost]
    public async Task<ActionResult<CustomResponse<List<MessageDTO>>>> Create(MessageCreateDTO messageCreateDTO)
    {
        return CreateActionResultInstance(await _messageService.CreateAsync(messageCreateDTO));
    }
}