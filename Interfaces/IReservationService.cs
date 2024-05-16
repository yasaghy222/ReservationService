using ReservationService.DTOs;
using ReservationService.Enums;
using ReservationService.Models;

namespace ReservationService.Interfaces
{
    public interface IReservationService
    {
        Task<Result> Get(Guid id);
        Task<Result> GetAll(ReservationFilterDto model);

        Task<Result> Add(AddReservationDto model);
        Task<Result> ChangeStatus(Guid id, ReservationStatus status);
    }
}


