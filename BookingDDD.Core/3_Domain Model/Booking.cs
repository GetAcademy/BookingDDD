namespace BookingDDD.Core._3_Domain_Model
{
    public class Booking
    {
        public Guid Id { get;  }
        public BookingPeriod BookingPeriod { get; }
        public bool IsCancelled { get; private set; }

        public Booking(BookingPeriod bookingPeriod)
        : this(Guid.NewGuid(), bookingPeriod)
        {
        }

        public Booking(Guid id, BookingPeriod bookingPeriod, bool isCancelled = false)
        {
            Id = id;
            BookingPeriod = bookingPeriod;
            IsCancelled = isCancelled;
        }

        public void Cancel()
        {
            IsCancelled = true;
        }

        public bool IsOverlapping(Booking otherBooking)
        {
            return this.BookingPeriod.IsOverlapping(otherBooking.BookingPeriod)
        }
    }
}
