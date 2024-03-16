﻿using GrotHotelApi.Data;
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
            var roomRate = await _context.RoomRates.FirstOrDefaultAsync(m=>m.RoomRateId == id);
            return roomRate;
        }

        public async Task<BlackOutDate> GetBlackOutDate(int id)
        {
            var blackOutdate = await _context.BlackOutDates.Include(m=>m.Dates).FirstOrDefaultAsync(m => m.RoomRateId == id);
            return blackOutdate;
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
    }
}
