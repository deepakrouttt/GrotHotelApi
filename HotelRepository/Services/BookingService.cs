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
            var hotelList = await _context.Hotels.Include(hotel => hotel.HotelRooms)
                .ThenInclude(room => room.RoomRates).Where(hotel => hotel.HotelRooms.Any(room =>
                    room.RoomRates.Any(rate => rate.DateFrom <= booking.DateTo && rate.DateTo >=
                    booking.DateFrom && rate != null)))
                .Select(hotel => new Hotel
                {
                    HotelId = hotel.HotelId,
                    HotelName = hotel.HotelName,
                    HotelImage = hotel.HotelImage,
                    Address = hotel.Address,
                    ChildAgeRange = hotel.ChildAgeRange,
                    Description = hotel.Description,
                    Rating = hotel.Rating,
                    HotelRooms = hotel.HotelRooms
                    .Where(room => room.RoomRates != null && room.RoomRates.Any(rate =>
                    rate.DateFrom <= booking.DateTo && rate.DateTo >= booking.DateFrom)).ToList()
                })
            .ToListAsync();


            var dynamicHotelRates = hotelList.Select(hotel => new dynamicHotelRate
            {
                Hotel = hotel,
                RoomRates = hotel.HotelRooms.SelectMany(room => room.RoomRates.Where(rate =>
                rate.DateFrom <= booking.DateTo && rate.DateTo >= booking.DateFrom && rate != null)
                .Select(rate => new dynamicRoomRate
                {
                    DateFrom = rate.DateFrom,
                    DateTo = rate.DateTo,
                    Rate = CalculateRate(rate, booking)
                }))
            }).ToList();

            return new HotelsWithRate { Hotels = dynamicHotelRates, numberAdults = booking.Adult };
        }


        public decimal CalculateRate(RoomRate rate, Booking booking)
        {
            decimal baseRate = 0;

            if (booking.Adult == 1 && booking.Children == 1 && rate.IsException)
            {
                baseRate = rate.DoubleRate;
            }
            else
            {
                baseRate = booking.Adult == 1 ? rate.SingleRate :
                           booking.Adult == 2 ? rate.DoubleRate :
                           booking.Adult >= 3 ? rate.TripleRate :
                                                rate.TripleRate;
            }

            if (booking.Adult > 3 && rate.AdultRate != null && rate.IsExtraAdult)
            {
                int extraAdults = booking.Adult - 3;
                baseRate += extraAdults * rate.AdultRate;
            }
            decimal childRate = 0;
            if (booking.Children > 0 && rate.childRate != null)
            {
                childRate = booking.Children * rate.childRate;
            }
            return baseRate + childRate;
        }



    }
}