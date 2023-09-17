using FluentValidation;
using Transportathon._0x80072F78.Core.DTOs;

namespace Transportathon._0x80072F78.Services.Validators;

public class StatusUpdateDTOValidator : AbstractValidator<StatusUpdateDTO>
{
    public StatusUpdateDTOValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.DocumentStatus).NotNull().IsInEnum();
    }
}