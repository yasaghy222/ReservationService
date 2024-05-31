using ReservationService.Entities;
using ReservationService.Enums;

namespace ReservationService.Models
{
    public class PricePlanInfo
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required string ForeignTitle { get; set; }

        public PricePlanTarget Target { get; set; }
        public PricePlanType Type { get; set; }
        public PricePlanStatus Status { get; set; }

        public ICollection<PricePlanItemInfo>? Items { get; set; }
    }
}