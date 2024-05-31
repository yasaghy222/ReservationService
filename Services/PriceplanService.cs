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
						   IValidator<AddPricePlanDto> addValidator,
						   IValidator<EditPricePlanDto> editValidator) : IPricePlanService, IDisposable
{
	private readonly ReservationServiceContext _context = context;
	private readonly IValidator<AddPricePlanDto> _addValidator = addValidator;
	private readonly IValidator<EditPricePlanDto> _editValidator = editValidator;

	public async Task<Result> Get(Guid id)
	{
		if (id == Guid.Empty)
			return CustomErrors.InvalidData("Id not Assigned!");

		PricePlan? pricePlan = await _context.PricePlans.SingleOrDefaultAsync(r => r.Id == id);
		if (pricePlan == null)
			return CustomErrors.NotFoundData();
		else
			return CustomResults.SuccessOperation(pricePlan.Adapt<PricePlanDetail>());
	}

	public async Task<Result> GetAll(PricePlanFilterDto model)
	{
		IQueryable<PricePlanInfo> query = from PricePlan in _context.PricePlans
										.Skip((model.PageIndex - 1) * model.PageSize)
										.Take(model.PageSize)
										  select new PricePlanInfo
										  {
											  Title = PricePlan.Title,
											  ForeignTitle = PricePlan.ForeignTitle,
											  Description = PricePlan.Description,
											  Target = PricePlan.Target,
											  Id = PricePlan.Id,
											  Type = PricePlan.Type,
											  Status = PricePlan.Status
										  };

		query = model.Target switch
		{
			PricePlanTarget.TextConsolation => query.Where(d => d.Target == PricePlanTarget.TextConsolation),
			PricePlanTarget.TelConsolation => query.Where(d => d.Target == PricePlanTarget.TelConsolation),
			PricePlanTarget.Reservation => query.Where(d => d.Target == PricePlanTarget.Reservation),
			_ => query
		};

		query = model.Type switch
		{
			PricePlanType.Public => query.Where(d => d.Type == PricePlanType.Public),
			PricePlanType.Doctor => query.Where(d => d.Type == PricePlanType.Doctor),
			PricePlanType.Therapist => query.Where(d => d.Type == PricePlanType.Therapist),
			PricePlanType.Specialty => query.Where(d => d.Type == PricePlanType.Specialty),
			PricePlanType.Clinic => query.Where(d => d.Type == PricePlanType.Clinic),
			_ => query
		};

		query = model.Status switch
		{
			PricePlanStatus.Active => query.Where(d => d.Status == PricePlanStatus.Active),
			PricePlanStatus => query.Where(d => d.Status == PricePlanStatus.Inactive),
			_ => query
		};

		return CustomResults.SuccessOperation(await query.ToListAsync());
	}

	public async Task<Result> Add(AddPricePlanDto model)
	{
		ValidationResult validationResult = _addValidator.Validate(model);
		if (!validationResult.IsValid)
			return CustomErrors.InvalidData(validationResult.Errors);

		try
		{
			PricePlan pricePlan = model.Adapt<PricePlan>();
			await _context.PricePlans.AddAsync(pricePlan);

			await _context.SaveChangesAsync();

			return CustomResults.SuccessCreation(pricePlan.Adapt<PricePlanDetail>());
		}
		catch (Exception e)
		{
			return CustomErrors.InternalServer(e.Message);
		}
	}

	public async Task<Result> Edit(EditPricePlanDto model)
	{
		ValidationResult validationResult = _editValidator.Validate(model);
		if (!validationResult.IsValid)
			return CustomErrors.InvalidData(validationResult.Errors);

		PricePlan? oldData = await _context.PricePlans.SingleOrDefaultAsync(d => d.Id == model.Id);
		if (oldData == null)
			return CustomErrors.NotFoundData();

		_context.Entry(oldData).State = EntityState.Detached;
		oldData = model.Adapt<PricePlan>();

		try
		{
			_context.PricePlans.Update(oldData);
			await _context.SaveChangesAsync();

			return CustomResults.SuccessCreation(oldData.Adapt<PricePlanDetail>());
		}
		catch (Exception e)
		{
			return CustomErrors.InternalServer(e.Message);
		}
	}


	public async Task<Result> ChangeStatus(Guid id, PricePlanStatus status)
	{
		Reservation? oldData = await _context.Reservations.SingleOrDefaultAsync(d => d.Id == id);
		if (oldData == null)
			return CustomErrors.NotFoundData();

		try
		{
			int effectedRowCount = await _context.PricePlans.Where(d => d.Id == id)
									 					 .ExecuteUpdateAsync(setters => setters.SetProperty(d => d.Status, status));

			if (effectedRowCount == 1)
				return CustomResults.SuccessUpdate(oldData.Adapt<PricePlanDetail>());
			else
				return CustomErrors.NotFoundData();
		}
		catch (Exception e)
		{
			return CustomErrors.InternalServer(e.Message);
		}
	}

	public async Task<Result> Delete(Guid Id)
	{
		PricePlan? pricePlan = await _context.PricePlans.SingleOrDefaultAsync(pp => pp.Id == Id);
		if (pricePlan == null)
			return CustomErrors.NotFoundData();

		try
		{
			_context.PricePlans.Remove(pricePlan);
			await _context.SaveChangesAsync();

			return CustomResults.SuccessDelete();
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
