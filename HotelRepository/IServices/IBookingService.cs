﻿using GrotHotel.Models;
using GrotHotelApi.Models;

namespace GrotHotelApi.HotelRepository.IServices
{
    public interface IBookingService
    {
        Task<HotelsWithRate> GetHotelsBySearch(Booking booking);
        decimal CalculateRate(RoomRate rate, Booking booking);
        decimal CalculateTotalRate(decimal rate, Booking booking, IEnumerable<DateEntry>? blackoutDates);
    }
}