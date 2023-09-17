using FluentValidation;
using Transportathon._0x80072F78.Core.DTOs.ForCompany;

namespace Transportathon._0x80072F78.Services.Validators.ForCompany;

public class TeamWorkerCreateDTOValidator : AbstractValidator<TeamWorkerCreateDTO>
{
    public TeamWorkerCreateDTOValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(40);
        RuleFor(x => x.Surname).NotEmpty().MaximumLength(40);
        RuleFor(x => x.PhoneNumber).MaximumLength(15);
        RuleFor(x => x.EMail).MaximumLength(50);
        RuleFor(x => x.Experience).MaximumLength(10);
    }
}