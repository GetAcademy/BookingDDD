using BookingDDD.Core._3_Domain_Model;

namespace BookingDDD.Core._2_DomainServices
{
    public interface IBookingRepository
    {
        Task<List<Booking>> GetAllAsync();
        Task AddAsync(Booking booking);
    }
}
