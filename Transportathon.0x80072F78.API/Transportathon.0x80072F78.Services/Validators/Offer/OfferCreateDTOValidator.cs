using FluentValidation;
using Transportathon._0x80072F78.Core.DTOs;

namespace Transportathon._0x80072F78.Services.Validators.Offer;

public class OfferCreateDTOValidator : AbstractValidator<OfferCreateDTO>
{
    public OfferCreateDTOValidator()
    {
        RuleFor(x => x.TransportationRequestId).NotEmpty();
        RuleFor(x => x.CompanyId).NotEmpty();
        RuleFor(x => x.VehicleId).NotEmpty();
        RuleFor(x => x.TeamId).NotEmpty();
        RuleFor(x => x.Price).NotEmpty();
        RuleFor(x => x.Note).MaximumLength(500);
        RuleFor(x => x.Status).NotNull().IsInEnum();
    }
}