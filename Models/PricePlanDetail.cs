using ReservationService.Entities;
using ReservationService.Enums;

namespace ReservationService.Models
{
    public class PricePlanDetail : BaseEntity
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public TimeOnly? Duration { get; set; }
        public TimeOnly? ResponseTimeLimit { get; set; }
        public Guid? ForeignId { get; set; }
        public required string ForeignTitle { get; set; }

        public PricePlanTarget Target { get; set; } = PricePlanTarget.TextConsolation;
        public PricePlanType Type { get; set; } = PricePlanType.Public;
        public PricePlanStatus Status { get; set; } = PricePlanStatus.Active;

        public ICollection<PricePlanItemInfo>? Items { get; set; }
    }
}