using ReservationService.Enums;

namespace ReservationService.DTOs
{
    public class ReservationFilterDto(int pageIndex, int pageSize, Guid? customerId, Guid? providerId, ReservationType? type, ReservationProvider? provider, ReservationStatus? status)
    {
        public int PageIndex { get; set; } = pageIndex < 0 ? 1 : pageIndex;
        public int PageSize { get; set; } = pageSize < 0 ? 10 : pageSize;

        public Guid? CustomerId { get; set; } = customerId;
        public Guid? ProviderId { get; set; } = providerId;

        public ReservationType? Type { get; set; } = type;
        public ReservationProvider? Provider { get; set; } = provider;
        public ReservationStatus? Status { get; set; } = status;
    }
}