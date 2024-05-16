namespace ReservationService.Models
{
    public class Customer
    {
        public required string Name { get; set; }
        public required string Family { get; set; }
        public required string FullName { get; set; }

        public string? Phone { get; set; }
    }
}
