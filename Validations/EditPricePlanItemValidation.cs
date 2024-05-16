using ReservationService.DTOs;
using FluentValidation;
using ReservationService.Enums;

namespace DoctorService.Validations;

public class EditPricePlanItemValidation : AbstractValidator<EditPricePlanItemDto>
{
	public EditPricePlanItemValidation()
	{
		RuleFor(d => d.Id).NotNull().NotEmpty();

		RuleFor(d => d.Title).NotEmpty()
							.NotNull()
							.MaximumLength(100);

		RuleFor(d => d.Description).MaximumLength(500);

		RuleFor(d => d.Price).NotNull().NotEmpty().GreaterThan(0);
	}
}
