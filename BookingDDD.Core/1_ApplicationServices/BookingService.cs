using BookingDDD.Core._2_DomainServices;
using BookingDDD.Core._3_Domain_Model;

namespace BookingDDD.Core._1_ApplicationServices
{
    public class BookingService
    {
        private static readonly Guid ResourceId = Guid.Empty;

        private readonly IBookingRepository _bookingRepository;
        private readonly OpeningHours _openingHours = new(8, 16);

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<Result<Booking>> BookAsync(BookingPeriod period)
        {
            var existingBookings = await _bookingRepository.GetAllAsync();
            var resource = new Resource(ResourceId, _openingHours, existingBookings);
            var bookingResult = resource.Book(period);
            if (!bookingResult.IsSuccess)
            {
                return bookingResult;
            }

            await _bookingRepository.AddAsync(bookingResult.Value!);
            return bookingResult;
        }

        public async Task<Result<Booking>> CancelAsync(Guid bookingId, DateTime now)
        {
            var booking = await _bookingRepository.GetAsync(bookingId);
            if (booking == null)
            {
                return Result<Booking>.Fail("Booking does not exist.");
            }

            var cancelResult = booking.Cancel(now);
            if (!cancelResult.IsSuccess)
            {
                return cancelResult;
            }

            await _bookingRepository.UpdateAsync(booking);
            return Result<Booking>.Success(booking);
        }
    }
}
