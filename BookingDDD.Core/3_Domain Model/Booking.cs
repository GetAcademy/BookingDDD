namespace BookingDDD.Core._3_Domain_Model
{
    public class Booking
    {
        public Guid Id { get; }
        public BookingPeriod Period { get; }
        public BookingStatus Status { get; private set; }
        public bool IsActive => Status == BookingStatus.Active;

        public Booking(BookingPeriod period)
            : this(Guid.NewGuid(), period)
        {
        }

        public Booking(
            Guid id,
            BookingPeriod period,
            BookingStatus status = BookingStatus.Active)
        {
            Id = id;
            Period = period;
            Status = status;
        }

        public Result<Booking> Cancel(DateTime now)
        {
            if (now >= Period.Start)
            {
                return Result<Booking>.Fail("Cannot cancel booking after it has started.");
            }

            Status = BookingStatus.Cancelled;
            return Result<Booking>.Success(this);
        }

        public bool IsOverlapping(Booking otherBooking)
        {
            return IsOverlapping(otherBooking.Period);
        }

        public bool IsOverlapping(BookingPeriod bookingPeriod)
        {
            return Period.IsOverlapping(bookingPeriod);
        }
    }
}
