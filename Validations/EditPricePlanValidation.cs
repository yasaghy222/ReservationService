using ReservationService.DTOs;
using FluentValidation;
using ReservationService.Enums;

namespace DoctorService.Validations;

public class EditPricePlanValidation : AbstractValidator<EditPricePlanDto>
{
	public EditPricePlanValidation()
	{
		RuleFor(d => d.Id).NotEmpty().NotNull();

		RuleFor(d => d.Title).NotEmpty()
							.NotNull()
							.MaximumLength(100);

		RuleFor(d => d.Description).MaximumLength(500);

		RuleFor(d => d.Target).IsInEnum();

		RuleFor(d => d.Type).IsInEnum();
	}
}
