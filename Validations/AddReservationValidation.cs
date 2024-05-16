using ReservationService.DTOs;
using FluentValidation;
using ReservationService.Enums;

namespace DoctorService.Validations;

public class AddReservationValidation : AbstractValidator<AddReservationDto>
{
	public AddReservationValidation()
	{
		RuleFor(d => d.CustomerId).NotEmpty()
							.NotNull();

		RuleFor(d => d.Customer).NotEmpty()
								.NotNull();

		RuleFor(d => d.ProviderId).NotEmpty()
							  .NotNull();

		RuleFor(d => d.Provider).NotEmpty()
								.NotNull();

		RuleFor(d => d.PricePlanItemId).NotEmpty()
							 .NotNull();

		RuleFor(d => d.Description).MaximumLength(500);

		RuleFor(d => d.Type).IsInEnum();
	}
}
