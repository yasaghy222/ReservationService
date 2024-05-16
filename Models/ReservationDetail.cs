using ReservationService.Entities;
using ReservationService.Enums;

namespace ReservationService.Models
{
    public class ReservationDetail : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public required Customer Customer { get; set; }

        public Guid ProviderId { get; set; }
        public required Provider Provider { get; set; }
        public ReservationProvider ProviderType { get; set; }

        public Guid PricePlanItemId { get; set; }
        public required PricePlanItem PricePlanItem { get; set; }

        public string? DiscountCode { get; set; }
        public float DiscountAmount { get; set; } = 0;

        public float Price { get; set; }
        public float? TotalPrice { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; }

        public string? Description { get; set; }
        public ReservationType Type { get; set; }

        public ReservationStatus Status { get; set; } = ReservationStatus.Waiting;
        public string? StatusDescription { get; set; }
    }
}