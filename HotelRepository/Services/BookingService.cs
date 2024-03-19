using GrotHotel.Models;
using GrotHotelApi.Data;
using GrotHotelApi.HotelRepository.IServices;
using GrotHotelApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GrotHotelApi.HotelRepository.Services
{
    public class BookingService : IBookingService
    {
        private readonly GrotHotelApiDbContext _context;

        public BookingService(GrotHotelApiDbContext context)
        {
            _context = context;
        }


        public async Task<List<Hotel>> GetHotelsBySearch(Booking booking)
        {
            var hotelList = await _context.Hotels.Include(hotel => hotel.HotelRooms).ThenInclude(room =>
            room.RoomRates.Where(rate => rate.DateFrom <= booking.DateTo && rate.DateTo >= booking.DateFrom 
            && rate != null)).Where(hotel => hotel.HotelRooms.All(room => room.RoomRates != null && 
            room.RoomRates.Any(rate => rate.DateFrom <= booking.DateTo && rate.DateTo >= booking.DateFrom)))
            .ToListAsync();

            return hotelList;
        }
    }
}