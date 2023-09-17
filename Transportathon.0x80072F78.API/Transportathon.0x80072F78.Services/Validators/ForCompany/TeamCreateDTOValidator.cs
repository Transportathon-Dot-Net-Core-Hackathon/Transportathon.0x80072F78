using FluentValidation;
using Transportathon._0x80072F78.Core.DTOs.ForCompany;

namespace Transportathon._0x80072F78.Services.Validators.ForCompany;

public class TeamCreateDTOValidator : AbstractValidator<TeamCreateDTO>
{
    public TeamCreateDTOValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.CompanyId).NotEmpty();
    }
}