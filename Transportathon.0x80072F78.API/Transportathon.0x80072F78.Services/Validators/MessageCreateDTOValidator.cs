using FluentValidation;
using Transportathon._0x80072F78.Core.DTOs;

namespace Transportathon._0x80072F78.Services.Validators;

public class MessageCreateDTOValidator : AbstractValidator<MessageCreateDTO>
{
    public MessageCreateDTOValidator()
    {
        RuleFor(x => x.ReceiverId).NotEmpty();
        RuleFor(x => x.MessageContent).NotEmpty();
    }
}