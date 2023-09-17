using FluentValidation;
using Transportathon._0x80072F78.Core.DTOs;

namespace Transportathon._0x80072F78.Services.Validators;

public class AddressCreateDTOValidator : AbstractValidator<AddressCreateDTO>
{
    public AddressCreateDTOValidator()
    {
        RuleFor(x => x.AddressType).NotNull().IsInEnum();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(20);
        RuleFor(x => x.City).NotEmpty().MaximumLength(20);
        RuleFor(x => x.District).NotEmpty().MaximumLength(20);
        RuleFor(x => x.LocalAddress).NotEmpty().MaximumLength(200);
    }
}