using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Transportathon._0x80072F78.Core.DTOs;
using Transportathon._0x80072F78.Core.Entities;
using Transportathon._0x80072F78.Core.Entities.Identity;
using Transportathon._0x80072F78.Core.Repository;
using Transportathon._0x80072F78.Shared.Interfaces;
using Transportathon._0x80072F78.Shared.Models;

namespace Transportathon._0x80072F78.Services;

public class MessageService : IMessageService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextData _httpContextData;
    private readonly UserManager<AspNetUser> _userManager;
    public Guid SenderId { get; set; }
    public MessageService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextData httpContextData, UserManager<AspNetUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextData = httpContextData;
        SenderId = Guid.Parse(_httpContextData.UserId);
        _userManager = userManager;
    }

    public async Task<CustomResponse<List<MyMessagesDTO>>> GetAllAsync()
    {
        List<Guid> reciverIds = (await _unitOfWork.MessageRepository.GetAllByFilterAsync(x => x.SenderId == SenderId, null, null, x => x.ReceiverId)).ToList();
        List<MyMessagesDTO> messages = new List<MyMessagesDTO>();
        foreach (var userId in reciverIds)
        {
            AspNetUser aspNetUser = await _userManager.FindByIdAsync(userId.ToString());
            MyMessagesDTO messagesDTO = new MyMessagesDTO();
            messagesDTO.Name = aspNetUser.FirstName;
            messagesDTO.Surname = aspNetUser.FamilyName;
            messagesDTO.UserImage = aspNetUser.UserImage;
            messages.Add(messagesDTO);
        }

        return CustomResponse<List<MyMessagesDTO>>.Success(StatusCodes.Status200OK, messages);
    }

    public async Task<CustomResponse<List<MessageDTO>>> GetMessagesByReceiverIdAsync(Guid receiverId)
    {
        List<Message> sendedMessages = (await _unitOfWork.MessageRepository.GetAllByFilterAsync(x => x.SenderId == SenderId && x.ReceiverId == receiverId)).ToList();
        List<Message> receivedMessages = (await _unitOfWork.MessageRepository.GetAllByFilterAsync(x => x.SenderId == receiverId && x.ReceiverId == SenderId)).ToList();

        var allMessages = sendedMessages.Concat(receivedMessages);
        List<Message> sortedMessages = allMessages.OrderBy(message => message.SendTime).ToList();

        List<MessageDTO> messageDTOs = new List<MessageDTO>();

        foreach (var message in sortedMessages)
        {
            AspNetUser aspNetUser = await _userManager.FindByIdAsync(message.SenderId.ToString());
            MessageDTO messageDTO = new MessageDTO();
            messageDTO.Name = aspNetUser.FirstName;
            messageDTO.SurName = aspNetUser.FamilyName;
            messageDTO.Message = message.MessageContent;
            messageDTO.SendTime = message.SendTime;
            messageDTOs.Add(messageDTO);
        }

        return CustomResponse<List<MessageDTO>>.Success(StatusCodes.Status200OK, messageDTOs);
    }

    public async Task<CustomResponse<List<MessageDTO>>> CreateAsync(MessageCreateDTO messageCreateDTO)
    {
        await _unitOfWork.BeginTransactionAsync();

        Message message = _mapper.Map<Message>(messageCreateDTO);
        message.SenderId = SenderId;
        message.SendTime = DateTime.Now;

        await _unitOfWork.MessageRepository.CreateAsync(message);
        await _unitOfWork.SaveAsync();
        await _unitOfWork.CommitAsync();

        List<MessageDTO> allMessages = (await GetMessagesByReceiverIdAsync(messageCreateDTO.ReceiverId)).Data;

        return CustomResponse<List<MessageDTO>>.Success(StatusCodes.Status200OK, allMessages);
    }
}