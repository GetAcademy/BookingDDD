namespace BookingDDD.Core._3_Domain_Model
{
    public class Resource
    {
        private readonly List<Booking> _bookings;

        public Resource(Guid id, OpeningHours openingHours, IEnumerable<Booking> bookings)
        {
            Id = id;
            OpeningHours = openingHours;
            _bookings = bookings.ToList();
        }

        public Guid Id { get; }
        public OpeningHours OpeningHours { get; }

        public Result<Booking> Book(BookingPeriod period)
        {
            if (!OpeningHours.Contains(period))
            {
                return Result<Booking>.Fail("Booking must be within opening hours.");
            }

            if (_bookings.Any(b => b.IsActive && b.Overlaps(period)))
            {
                return Result<Booking>.Fail("Booking overlaps with an existing booking.");
            }

            var booking = new Booking(period);
            _bookings.Add(booking);
            return Result<Booking>.Success(booking);
        }
    }
}
