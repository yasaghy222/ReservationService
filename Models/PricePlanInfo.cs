using ReservationService.Entities;
using ReservationService.Enums;

namespace ReservationService.Models
{
    public class PricePlanInfo : BaseEntity
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required string ForeignTitle { get; set; }

        public PricePlanTarget Target { get; set; } = PricePlanTarget.TextConsolation;
        public PricePlanType Type { get; set; } = PricePlanType.Public;

        public ICollection<PricePlanItemInfo>? Items { get; set; }
    }
}