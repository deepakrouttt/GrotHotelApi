using GrotHotelApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace GrotHotelApi.HotelRepository.IServices
{
    public interface IHotelService
    {
        Task<List<Hotel>> GetHotels();
        Task<Hotel> GetHotel(int id);
        Task<HotelRoom> GetRoom(int id);
        Task<RoomRate> GetRate(int id);
        Task<BlackOutDate> GetBlackOutDate(int id);

        Task<Hotel> addHotel(Hotel hotel);
        Task<Hotel> addRoom(HotelRoom hotelroom);
        Task<RoomRate> addRate(RoomRate roomRate);

        Task<Hotel> UpdateHotel(Hotel hotel);
        Task<HotelRoom> UpdateRoom(HotelRoom obj);
        Task<RoomRate> UpdateRate(RoomRate obj);


        Task<Hotel> DeleteHotel(int id);
        Task<HotelRoom> DeleteRoom(int id);
        Task<RoomRate> DeleteRate(int id);
    }
}