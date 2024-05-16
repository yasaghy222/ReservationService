namespace ReservationService.Models
{
    public class Provider
    {
        public required string Name { get; set; }
        public required string Family { get; set; }
        public required string FullName { get; set; }
        public required string SpecialtyTitle { get; set; }
        public string? Phone { get; set; }
    }
}
