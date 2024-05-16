using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReservationService.Enums;

namespace ReservationService.Entities
{
    public class PricePlanItem : BaseEntity
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public float Price { get; set; }

        public Guid PricePlanId { get; set; }
        public required PricePlan PricePlan { get; set; }

        public PricePlanItemStatus Status { get; set; } = PricePlanItemStatus.Active;

        public ICollection<Reservation>? Reservations { get; set; }
    }
}