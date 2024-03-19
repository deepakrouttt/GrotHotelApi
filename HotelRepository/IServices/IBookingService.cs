using GrotHotel.Models;
using GrotHotelApi.Models;

namespace GrotHotelApi.HotelRepository.IServices
{
    public interface IBookingService
    {
        Task<List<Hotel>> GetHotelsBySearch(Booking booking);
    }
}