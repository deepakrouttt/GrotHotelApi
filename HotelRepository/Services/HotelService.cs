using GrotHotelApi.Data;
using GrotHotelApi.HotelRepository.IServices;
using GrotHotelApi.Migrations;
using GrotHotelApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GrotHotelApi.HotelRepository.Services
{
    public class HotelService : IHotelService
    {
        private readonly GrotHotelApiDbContext _context;

        public HotelService(GrotHotelApiDbContext context)
        {
            _context = context;
        }

        //Get Method
        public async Task<List<Hotel>> GetHotels()
        {
            var hotelList = await _context.Hotels.Include(m => m.HotelRooms).ToListAsync();
            return hotelList;
        }

        public async Task<Hotel> GetHotel(int id)
        {
            var hotel = await _context.Hotels.Include(m => m.HotelRooms).FirstOrDefaultAsync(m => m.HotelId == id);
            return hotel;
        }

        public async Task<HotelRoom> GetRoom(int id)
        {
            var hotelRoom = await _context.HotelRooms.Include(m => m.RoomRates).FirstOrDefaultAsync(m => m.HotelRoomId == id);
            return hotelRoom;
        }

        public async Task<RoomRate> GetRate(int id)
        {
            var roomRate = await _context.RoomRates.FirstOrDefaultAsync(m => m.RoomRateId == id);
            return roomRate;
        }

        public async Task<BlackoutData> GetBlackOutDate(int id)
        {
            var blackOutDate = await _context.BlackOutDates.Include(m => m.Dates)
                .FirstOrDefaultAsync(m => m.RoomRateId == id);
            if (blackOutDate == null)
            {
                return null;
            }
            var ListDates = blackOutDate.Dates.Select(d => d.Date.ToString("yyyy-MM-dd")).ToList();

            var result = new BlackoutData
            {
                Id = blackOutDate.RoomRateId,
                BlackOutDates = ListDates
            };
            return result;
        }


        //Post Method
        public async Task<Hotel> addHotel(Hotel hotel)
        {
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
            return hotel;
        }

        public async Task<Hotel> addRoom(HotelRoom hotelroom)
        {
            var hotel = await _context.Hotels.Include(a => a.HotelRooms).SingleOrDefaultAsync(m => m.HotelId == hotelroom.HotelId);
            hotel.HotelRooms.Add(hotelroom);
            await _context.SaveChangesAsync();
            return hotel;
        }

        public async Task<RoomRate> addRate(RoomRate roomRate)
        {
            _context.RoomRates.Add(roomRate);
            await _context.SaveChangesAsync();
            return roomRate;
        }

        public async Task<BlackOutDate> addBlackOutDate(BlackOutDate date)
        {
            var existingBlackOutDate = _context.BlackOutDates.Include(b => b.Dates)
                                                     .FirstOrDefault(b => b.RoomRateId == date.RoomRateId);
            if (existingBlackOutDate == null)
            {
                existingBlackOutDate = new BlackOutDate { RoomRateId = date.RoomRateId };
                _context.BlackOutDates.Add(existingBlackOutDate);
            }

            foreach (var entry in date.Dates)
            {
                existingBlackOutDate.Dates.Add(entry);
            }

            _context.SaveChanges();
            return existingBlackOutDate;
        }


        //Put Method
        public async Task<Hotel> UpdateHotel(Hotel hotel)
        {
            var Updatehotel = await _context.Hotels.Include(m => m.HotelRooms).FirstOrDefaultAsync(m => m.HotelId == hotel.HotelId);
            Updatehotel.HotelName = hotel.HotelName;
            Updatehotel.HotelImage = hotel.HotelImage;
            Updatehotel.Address = hotel.Address;
            Updatehotel.ChildAgeRange = hotel.ChildAgeRange;
            Updatehotel.Rating = hotel.Rating;
            Updatehotel.Description = hotel.Description;

            _context.Hotels.Update(Updatehotel);
            await _context.SaveChangesAsync();
            return Updatehotel;
        }

        public async Task<HotelRoom> UpdateRoom(HotelRoom obj)
        {
            var hotelRoom = await _context.HotelRooms.Include(m => m.RoomRates).FirstOrDefaultAsync(m => m.HotelRoomId == obj.HotelRoomId);
            hotelRoom.HotelRoomId = obj.HotelRoomId;
            hotelRoom.Title = obj.Title;
            hotelRoom.RoomPicture = obj.RoomPicture;
            hotelRoom.Description = obj.Description;
            hotelRoom.HotelId = obj.HotelId;
            _context.HotelRooms.Update(hotelRoom);
            await _context.SaveChangesAsync();

            return hotelRoom;
        }

        public async Task<RoomRate> UpdateRate(RoomRate obj)
        {
            _context.RoomRates.Update(obj);
            await _context.SaveChangesAsync();
            return obj;
        }


        //Delete Method
        public async Task<Hotel> DeleteHotel(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();

            return hotel;
        }

        public async Task<HotelRoom> DeleteRoom(int id)
        {
            var hotelRoom = await _context.HotelRooms.FindAsync(id);

            _context.HotelRooms.Remove(hotelRoom);
            await _context.SaveChangesAsync();

            return hotelRoom;
        }

        public async Task<RoomRate> DeleteRate(int id)
        {
            var roomRate = await _context.RoomRates.FindAsync(id);
            _context.RoomRates.Remove(roomRate);
            await _context.SaveChangesAsync();

            return roomRate;
        }

        public async Task<BlackOutDate> DeleteBlackOutDate(DateTime date)
        {
            var blackOutDate = await _context.BlackOutDates.Include(b => b.Dates)
                .FirstOrDefaultAsync(b => b.Dates.Any(d => d.Date == date));

            if (blackOutDate != null)
            {
                var dateEntryToRemove = blackOutDate.Dates.FirstOrDefault(d => d.Date == date);
                blackOutDate.Dates.Remove(dateEntryToRemove);

                if (!blackOutDate.Dates.Any())
                {
                    _context.BlackOutDates.Remove(blackOutDate);
                }
                await _context.SaveChangesAsync();
            }

            return blackOutDate;
        }
    }

}
