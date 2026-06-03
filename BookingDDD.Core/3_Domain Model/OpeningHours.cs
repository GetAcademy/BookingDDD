namespace BookingDDD.Core._3_Domain_Model
{
    public record OpeningHours
    {
        public int OpensAtHour { get; }
        public int ClosesAtHour { get; }

        public OpeningHours(int opensAtHour, int closesAtHour)
        {
            OpensAtHour = opensAtHour;
            ClosesAtHour = closesAtHour;
        }

        public bool Contains(BookingPeriod period)
        {
            return period.Start.Hour >= OpensAtHour &&
                   period.End.Hour <= ClosesAtHour;
        }
    }
}
