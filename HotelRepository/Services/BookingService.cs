using GrotHotel.Models;
using GrotHotelApi.Data;
using GrotHotelApi.HotelRepository.IServices;
using GrotHotelApi.Models;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GrotHotelApi.HotelRepository.Services
{
    public class BookingService : IBookingService
    {
        private readonly GrotHotelApiDbContext _context;

        public BookingService(GrotHotelApiDbContext context)
        {
            _context = context;
        }


        public async Task<HotelsWithRate> GetHotelsBySearch(Booking booking)
        {
            string numberAdults;
            switch (booking.Adult)
            {
                case 1:
                    numberAdults = "SingleRate";
                    break;
                case 2:
                    numberAdults = "DoubleRate";
                    break;
                case 3:
                    numberAdults = "TripleRate";
                    break;
                default:
                    numberAdults = booking.Adult.ToString();
                    break;
            }
            var hotelList = await _context.Hotels.Include(hotel => hotel.HotelRooms).ThenInclude(room =>
            room.RoomRates.Where(rate => rate.DateFrom <= booking.DateTo && rate.DateTo >= booking.DateFrom &&
            rate != null)).Where(hotel => hotel.HotelRooms.All(room => room.RoomRates != null &&
            room.RoomRates.Any(rate => rate.DateFrom <= booking.DateTo && rate.DateTo >= booking.DateFrom)))
            .Select(hotel => new dynamicHotelRate
            {
                Hotel = hotel,
                RoomRates = (IEnumerable<dynamicRoomRate>)hotel.HotelRooms.SelectMany(room => room.RoomRates.Where(rate =>
                rate.DateFrom <= booking.DateTo && rate.DateTo >= booking.DateFrom && rate != null)
                .Select(rate => new dynamicRoomRate
                {
                    Rate = booking.Adult == 1 ? rate.SingleRate :
                       booking.Adult == 2 ? rate.DoubleRate :
                       booking.Adult == 3 ? rate.TripleRate :
                       booking.Adult >= 4 ? rate.TripleRate + rate.AdultRate :
                       0
                }))
            }).ToListAsync();

            return new HotelsWithRate { Hotels = hotelList, numberAdults = numberAdults };
        }
    }
}