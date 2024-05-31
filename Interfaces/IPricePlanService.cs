using ReservationService.Models;
using ReservationService.DTOs;
using ReservationService.Enums;

namespace ReservationService.Interfaces
{
    public interface IPricePlanService
    {
        Task<Result> Get(Guid id);
        Task<Result> GetAll(PricePlanFilterDto model);

        Task<Result> Add(AddPricePlanDto model);
        Task<Result> Edit(EditPricePlanDto model);
        Task<Result> ChangeStatus(Guid id, PricePlanStatus status);

        Task<Result> Delete(Guid id);
    }
}


