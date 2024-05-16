using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationService.DTOs
{
    public class AddPricePlanItemDto
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public float Price { get; set; }

        public Guid PricePlanId { get; set; }
    }
}