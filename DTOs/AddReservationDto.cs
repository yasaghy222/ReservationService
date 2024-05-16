using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReservationService.Enums;
using ReservationService.Models;

namespace ReservationService.DTOs
{
    public class AddReservationDto
    {
        public Guid CustomerId { get; set; }
        public required Customer Customer { get; set; }

        public Guid ProviderId { get; set; }
        public required Provider Provider { get; set; }

        public Guid PricePlanItemId { get; set; }
        public string? DiscountCode { get; set; }

        public string? Description { get; set; }
        public ReservationType Type { get; set; }
    }
}