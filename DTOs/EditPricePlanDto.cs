using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReservationService.Enums;

namespace ReservationService.DTOs
{
    public class EditPricePlanDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public TimeOnly? Duration { get; set; }
        public TimeOnly? ResponseTimeLimit { get; set; }
        public Guid? ForeignId { get; set; }

        public PricePlanTarget Target { get; set; } = PricePlanTarget.TextConsolation;
        public PricePlanType Type { get; set; } = PricePlanType.Public;
    }
}