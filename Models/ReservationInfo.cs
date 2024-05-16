using ReservationService.Enums;

namespace ReservationService.Models
{
    public class ReservationInfo
    {
        public Guid Id { get; set; }
        public required string CustomerFullName { get; set; }
        public required string ProviderFullName { get; set; }
        public ReservationProvider ProviderType { get; set; }

        public required string PricePlanItemTitle { get; set; }
        public float? TotalPrice { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; }

        public ReservationType Type { get; set; }
        public ReservationStatus Status { get; set; } = ReservationStatus.Waiting;
    }
}