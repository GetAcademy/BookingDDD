using BookingDDD.Core._2_DomainServices;
using BookingDDD.Core._3_Domain_Model;

namespace BookingDDD.Core._1_ApplicationServices
{
    public class BookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<Result<Booking>> BookAsync(BookingPeriod bookingPeriod)
        {
            var existingBookings = await _bookingRepository.GetAllAsync();
            var bookingCollection = new BookingCollection(existingBookings);
            if (bookingCollection.IsOverlapping(bookingPeriod))
            {
                return Result<Booking>.Fail("Booking overlaps with an existing booking.");
            }

            var newBooking = new Booking(bookingPeriod);
            await _bookingRepository.AddAsync(newBooking);
            return Result<Booking>.Success(newBooking);
        }

    }
}
