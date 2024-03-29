﻿using GrotHotel.Models;
using GrotHotelApi.Models;

namespace GrotHotelApi.HotelRepository.IServices
{
    public interface IBookingService
    {
        Task<HotelsWithRate> GetHotelsBySearch(Booking booking);
    }
}