namespace ReservationService.Enums
{
    public enum ReservationStatus : byte
    {
        Waiting = 0,
        Confirmed = 1,
        InProses = 2,
        Finish = 3,
        Cancel = 4,
    }
}