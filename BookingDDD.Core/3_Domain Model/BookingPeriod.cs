namespace BookingDDD.Core._3_Domain_Model
{
    public record BookingPeriod
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        private BookingPeriod(DateTime start, DateTime end)
        {
            End = end;
            Start = start;
        }

        public static Result<BookingPeriod> Create(DateTime start, DateTime end)
        {
            if (start >= end)
            {
                return Result<BookingPeriod>.Fail("Start must be before end.");
            }

            if (start.Minute != 0 || end.Minute != 0)
            {
                return Result<BookingPeriod>.Fail("Only whole hours can be booked.");
            }

            var period = new BookingPeriod(start, end);
            return Result<BookingPeriod>.Success(period);
        }

        public bool IsOverlapping(BookingPeriod other)
        {
            return other.Start < End && other.End > Start;
        }

        public bool IsOverlapping(Booking booking)
        {
            return IsOverlapping(booking.Period);
        }
    }
}
