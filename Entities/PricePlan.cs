using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReservationService.Enums;

namespace ReservationService.Entities
{
    public class PricePlan : BaseEntity
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

        public ICollection<PricePlanItem>? Items { get; set; }
    }
}