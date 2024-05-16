using ReservationService.Entities;
using ReservationService.Enums;

namespace ReservationService.Models
{
    public class PricePlanItemInfo : BaseEntity
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public float Price { get; set; }

        public PricePlanItemStatus Status { get; set; } = PricePlanItemStatus.Active;
    }
}