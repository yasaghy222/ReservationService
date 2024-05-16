using ReservationService.Models;
using ReservationService.DTOs;
using ReservationService.Enums;

namespace ReservationService.Interfaces
{
    public interface IPricePlanItemService
    {
        Task<Result> Get(Guid id);
        Task<Result> GetAll(Guid pricePlanId);

        Task<Result> Add(AddPricePlanItemDto model);
        Task<Result> Edit(EditPricePlanItemDto model);
        Task<Result> ChangeStatus(Guid id, PricePlanItemStatus status);

        Task<Result> Delete(Guid id);
    }
}


