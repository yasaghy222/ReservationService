using ReservationService.DTOs;
using FluentValidation;
using ReservationService.Enums;

namespace DoctorService.Validations;

public class AddPricePlanItemValidation : AbstractValidator<AddPricePlanItemDto>
{
	public AddPricePlanItemValidation()
	{
		RuleFor(d => d.Title).NotEmpty()
							.NotNull()
							.MaximumLength(100);

		RuleFor(d => d.Description).MaximumLength(500);

		RuleFor(d => d.Price).NotNull().NotEmpty().GreaterThan(0);

		RuleFor(d => d.PricePlanId).NotNull().NotEmpty();
	}
}
