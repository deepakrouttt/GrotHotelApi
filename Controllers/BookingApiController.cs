using GrotHotel.Models;
using GrotHotelApi.HotelRepository.IServices;
using GrotHotelApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GrotHotelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingApiController : ControllerBase
    {
        private readonly IBookingService _service;

        public BookingApiController(IBookingService service)
        {
            _service = service;
        }

        [HttpPost("GetHotelsBySearch")]
        public async Task<HotelsWithRate> GetHotelsBySearch([FromQuery]Booking booking)
        {
            var hotels = await _service.GetHotelsBySearch(booking);

            return hotels;
        }
    }
}
