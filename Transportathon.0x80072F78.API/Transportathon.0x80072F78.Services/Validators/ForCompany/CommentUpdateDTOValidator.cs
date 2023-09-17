using FluentValidation;
using Transportathon._0x80072F78.Core.DTOs.ForCompany;

namespace Transportathon._0x80072F78.Services.Validators.ForCompany;

public class CommentUpdateDTOValidator : AbstractValidator<CommentUpdateDTO>
{
    public CommentUpdateDTOValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.OfferId).NotEmpty();
        RuleFor(x => x.CompanyId).NotEmpty();
        RuleFor(x => x.Score).NotEmpty();
    }
}