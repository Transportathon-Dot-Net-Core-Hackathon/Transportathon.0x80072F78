using FluentValidation;
using Transportathon._0x80072F78.Core.DTOs.ForCompany;

namespace Transportathon._0x80072F78.Services.Validators.ForCompany;

public class VehicleCreateDTOValidator : AbstractValidator<VehicleCreateDTO>
{
    public VehicleCreateDTOValidator()
    {
        RuleFor(x => x.VehicleType).NotNull().IsInEnum();
        RuleFor(x => x.VehicleLicensePlate).NotEmpty().MaximumLength(100);
        RuleFor(x => x.VehicleVolumeCapacity).MaximumLength(40);
        RuleFor(x => x.VehicleWeightCapacity).MaximumLength(40);
        RuleFor(x => x.VehicleStatus).NotNull().IsInEnum();
        RuleFor(x => x.DriverId).NotEmpty();
    }
}