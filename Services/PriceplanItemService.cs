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

public class PricePlanItemService(ReservationServiceContext context,
						   IValidator<AddPricePlanItemDto> addValidator,
						   IValidator<EditPricePlanItemDto> editValidator) : IPricePlanItemService, IDisposable
{
	private readonly ReservationServiceContext _context = context;
	private readonly IValidator<AddPricePlanItemDto> _addValidator = addValidator;
	private readonly IValidator<EditPricePlanItemDto> _editValidator = editValidator;

	public async Task<Result> Get(Guid id)
	{
		if (id == Guid.Empty)
			return CustomErrors.InvalidData("Id not Assigned!");

		PricePlanItem? PricePlanItem = await _context.PricePlanItems.SingleOrDefaultAsync(r => r.Id == id);
		if (PricePlanItem == null)
			return CustomErrors.NotFoundData();
		else
			return CustomResults.SuccessOperation(PricePlanItem.Adapt<PricePlanItemInfo>());
	}

	public async Task<Result> GetAll(Guid pricePlanItem)
	{
		IQueryable<PricePlanItemInfo> query = from PricePlanItem in _context.PricePlanItems
											.Where(r => r.PricePlanId == pricePlanItem)
											  select new PricePlanItemInfo
											  {
												  Price = PricePlanItem.Price,
												  CreateAt = PricePlanItem.CreateAt,
												  Description = PricePlanItem.Description,
												  ModifyAt = PricePlanItem.ModifyAt,
												  Title = PricePlanItem.Title,
												  Id = PricePlanItem.Id,
												  Status = PricePlanItem.Status
											  };

		return CustomResults.SuccessOperation(await query.ToListAsync());
	}

	public async Task<Result> Add(AddPricePlanItemDto model)
	{
		ValidationResult validationResult = _addValidator.Validate(model);
		if (!validationResult.IsValid)
			return CustomErrors.InvalidData(validationResult.Errors);

		try
		{
			PricePlanItem PricePlanItem = model.Adapt<PricePlanItem>();
			await _context.PricePlanItems.AddAsync(PricePlanItem);

			await _context.SaveChangesAsync();

			return CustomResults.SuccessCreation(PricePlanItem.Adapt<PricePlanItemInfo>());
		}
		catch (Exception e)
		{
			return CustomErrors.InternalServer(e.Message);
		}
	}

	public async Task<Result> Edit(EditPricePlanItemDto model)
	{
		ValidationResult validationResult = _editValidator.Validate(model);
		if (!validationResult.IsValid)
			return CustomErrors.InvalidData(validationResult.Errors);

		PricePlanItem? oldData = await _context.PricePlanItems.SingleOrDefaultAsync(d => d.Id == model.Id);
		if (oldData == null)
			return CustomErrors.NotFoundData();

		_context.Entry(oldData).State = EntityState.Detached;
		oldData = model.Adapt<PricePlanItem>();

		try
		{
			_context.PricePlanItems.Update(oldData);
			await _context.SaveChangesAsync();

			return CustomResults.SuccessCreation(oldData.Adapt<PricePlanItemInfo>());
		}
		catch (Exception e)
		{
			return CustomErrors.InternalServer(e.Message);
		}
	}

	public async Task<Result> ChangeStatus(Guid id, PricePlanItemStatus status)
	{
		Reservation? oldData = await _context.Reservations.SingleOrDefaultAsync(d => d.Id == id);
		if (oldData == null)
			return CustomErrors.NotFoundData();

		try
		{
			int effectedRowCount = await _context.PricePlanItems.Where(d => d.Id == id)
									 					 .ExecuteUpdateAsync(setters => setters.SetProperty(d => d.Status, status));

			if (effectedRowCount == 1)
				return CustomResults.SuccessUpdate(oldData.Adapt<PricePlanItemInfo>());
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
		PricePlanItem? PricePlanItem = await _context.PricePlanItems.SingleOrDefaultAsync(pp => pp.Id == Id);
		if (PricePlanItem == null)
			return CustomErrors.NotFoundData();

		try
		{
			_context.PricePlanItems.Remove(PricePlanItem);
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
