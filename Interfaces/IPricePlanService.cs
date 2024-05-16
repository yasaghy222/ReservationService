using ReservationService.Models;
using ReservationService.DTOs;
using ReservationService.Enums;

namespace ReservationService.Interfaces
{
    public interface IPricePlanService
    {
        Task<Result> GetInfo(Guid id);
        Task<Result> GetDetail(Guid id);

        Task<Result> GetAllInfo(PricePlanFilterDto model);
        Task<Result> GetAllDetail(PricePlanFilterDto model);

        Task<Result> Add(AddPricePlanDto model);
        Task<Result> Edit(EditPricePlanDto model);
        Task<Result> ChangeStatus(Guid id, PricePlanStatus status);

        Task<Result> Delete(Guid id);
    }
}


