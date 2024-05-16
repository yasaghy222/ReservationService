using ReservationService.Enums;

namespace ReservationService.DTOs
{
    public class PricePlanFilterDto(int pageIndex, int pageSize, Guid? customerId, Guid? providerId, string title, PricePlanType? type, PricePlanTarget? target, PricePlanStatus? status)
    {
        public int PageIndex { get; set; } = pageIndex < 0 ? 1 : pageIndex;
        public int PageSize { get; set; } = pageSize < 0 ? 10 : pageSize;

        public Guid? CustomerId { get; set; } = customerId;
        public Guid? ProviderId { get; set; } = providerId;
        public string? Title { get; set; } = title;

        public PricePlanType? Type { get; set; } = type;
        public PricePlanTarget? Target { get; set; } = target;
        public PricePlanStatus? Status { get; set; } = status;
    }
}