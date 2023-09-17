using FluentValidation;
using Transportathon._0x80072F78.Core.DTOs;

namespace Transportathon._0x80072F78.Services.Validators;

public class TransportationRequestCreateDTOValidator : AbstractValidator<TransportationRequestCreateDTO>
{
    public TransportationRequestCreateDTOValidator()
    {
        RuleFor(x => x.RequestType).NotNull().IsInEnum();
        RuleFor(x => x.OutputAddressId).NotEmpty();
        RuleFor(x => x.DestinationAddressId).NotEmpty();
        RuleFor(x => x.Note).MaximumLength(500);
        RuleFor(x => x.DocumentStatus).NotNull().IsInEnum();

    }
}