using FluentValidation;
using Transportathon._0x80072F78.Core.DTOs.ForCompany;

namespace Transportathon._0x80072F78.Services.Validators.ForCompany;

public class CompanyCreateDTOValidator : AbstractValidator<CompanyCreateDTO>
{
    public CompanyCreateDTOValidator()
    {
        RuleFor(x => x.CompanyName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(40);
        RuleFor(x => x.Surname).NotEmpty().MaximumLength(40);
        RuleFor(x => x.City).NotEmpty().MaximumLength(150);
        RuleFor(x => x.District).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Street).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Alley).NotEmpty().MaximumLength(100);
        RuleFor(x => x.BuildingNumber).NotEmpty().MaximumLength(100);
        RuleFor(x => x.ApartmentNumber).NotEmpty().MaximumLength(100);
        RuleFor(x => x.PostCode).NotEmpty().MaximumLength(100);
        RuleFor(x => x.VKN).NotEmpty().MaximumLength(100);
    }
}