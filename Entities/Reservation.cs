using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReservationService.Enums;

namespace ReservationService.Entities
{
    public class Reservation : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public required string CustomerJson { get; set; }

        public Guid ProviderId { get; set; }
        public required string ProviderJson { get; set; }
        public ReservationProvider ProviderType { get; set; }

        public Guid PricePlanItemId { get; set; }
        public required PricePlanItem PricePlanItem { get; set; }
        public required string PricePlanItemJson { get; set; }

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