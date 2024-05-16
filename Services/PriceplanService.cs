using System.Linq.Expressions;
using System.Text.Json;
using FluentValidation;
using FluentValidation.Results;
using Mapster;
using Microsoft.EntityFrameworkCore;
using ReservationService.Data;
using ReservationService.DTOs;
using ReservationService.Entities;
using ReservationService.Enums;
using ReservationService.Interfaces;
using ReservationService.Models;
using ReservationService.Shared;

namespace ReservationService;

public class PricePlanService(ReservationServiceContext context,
						   IValidator<AddPricePlanDto> addValidator) : IPricePlanItemService, IDisposable
{
	private readonly ReservationServiceContext _context = context;
	private readonly IValidator<AddPricePlanDto> _addValidator = addValidator;

	public async Task<Result> Get(Guid id)
	{
		if (id == Guid.Empty)
			return CustomErrors.InvalidData("Id not Assigned!");

		Reservation? reservation = await _context.Reservations.SingleOrDefaultAsync(r => r.Id == id);
		if (reservation == null)
			return CustomErrors.NotFoundData();
		else
			return CustomResults.SuccessOperation(reservation.Adapt<ReservationDetail>());
	}
	public async Task<Result> GetAll(ReservationFilterDto model)
	{
		IQueryable<ReservationInfo> query = from Reservation in _context.Reservations
										.Skip((model.PageIndex - 1) * model.PageSize)
										.Take(model.PageSize)
											let Customer = Reservation.CustomerJson.FromJson<Customer>()
											let PricePlanItem = Reservation.PricePlanItemJson.FromJson<PricePlanItem>()
											let provider = Reservation.PricePlanItemJson.FromJson<Provider>()
											select new ReservationInfo
											{
												Id = Reservation.Id,
												CustomerFullName = Customer.FullName,
												PricePlanItemTitle = PricePlanItem.Title,
												ProviderFullName = provider.FullName,
												StartDate = Reservation.StartDate,
												EndDate = Reservation.EndDate,
												Status = Reservation.Status,
												Provider = Reservation.Provider,
												TotalPrice = Reservation.TotalPrice,
												Type = Reservation.Type,
											};

		return CustomResults.SuccessOperation(await query.ToListAsync());
	}
	public async Task<Result> Add(AddReservationDto model)
	{
		ValidationResult validationResult = _addValidator.Validate(model);
		if (!validationResult.IsValid)
			return CustomErrors.InvalidData(validationResult.Errors);

		try
		{
			Reservation Reservation = model.Adapt<Reservation>();
			await _context.Reservations.AddAsync(Reservation);

			await _context.SaveChangesAsync();

			return CustomResults.SuccessCreation(Reservation.Adapt<ReservationDetail>());
		}
		catch (Exception e)
		{
			return CustomErrors.InternalServer(e.Message);
		}
	}

	public async Task<Result> ChangeStatus(Guid id, ReservationStatus status)
	{
		Reservation? oldData = await _context.Reservations.SingleOrDefaultAsync(d => d.Id == id);
		if (oldData == null)
			return CustomErrors.NotFoundData();

		try
		{
			int effectedRowCount = await _context.Reservations.Where(d => d.Id == id)
									 					 .ExecuteUpdateAsync(setters => setters.SetProperty(d => d.Status, status));

			if (effectedRowCount == 1)
				return CustomResults.SuccessUpdate(oldData.Adapt<ReservationInfo>());
			else
				return CustomErrors.NotFoundData();
		}
		catch (Exception e)
		{
			return CustomErrors.InternalServer(e.Message);
		}
	}

	public void Dispose()
	{
		_context.Dispose();
	}

}
