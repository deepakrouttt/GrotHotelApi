using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GrotHotelApi.Data;
using GrotHotelApi.Models;
using GrotHotelApi.Migrations;
using GrotHotelApi.HotelRepository.IServices;
using Newtonsoft.Json;

namespace GrotHotelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelApiController : ControllerBase
    {
        private readonly IHotelService _service;

        public HotelApiController(IHotelService service)
        {
            _service = service;
        }

        [HttpGet("GetHotels")]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotels()
        {
            var Hotels = await _service.GetHotels();
            return Hotels;
        }

        [HttpGet("GetHotel/{id}")]
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
            var hotel = await _service.GetHotel(id);

            if (hotel == null)
            {
                return NotFound();
            }

            return hotel;
        }

        [HttpGet("GetRoom/{id}")]
        public async Task<ActionResult<HotelRoom>> GetRoom(int id)
        {
            var hotelRoom = await _service.GetRoom(id);

            if (hotelRoom == null)
            {
                return NotFound();
            }
            return hotelRoom;
        }
        [HttpGet("GetRate/{id}")]
        public async Task<ActionResult<RoomRate>> GetRate(int id)
        {
            var roomRate = await _service.GetRate(id);

            if (roomRate == null)
            {
                return NotFound();
            }
            return roomRate;
        }

        [HttpGet("GetBlackOutDate")]
        public async Task<ActionResult> GetBlackOutDate()
        {
            var blackOutDate = await _service.GetBlackOutDate();

            if (blackOutDate == null)
            {
                return NotFound();
            }
            var ListDates = blackOutDate.SelectMany(b => b.Dates).Select(d => d.Date.ToString("yyyy-MM-dd")).ToList();

            return Content(JsonConvert.SerializeObject(ListDates), "application/json");

        }
        [HttpPost("addHotel")]
        public async Task<ActionResult> addHotel([FromBody] Hotel hotel)
        {
            var addhotel = await _service.addHotel(hotel);
            return Ok(addhotel);
        }

        [HttpPost("addRoom")]
        public async Task<ActionResult> addRoom([FromBody] HotelRoom hotelroom)
        {
            var hotel = await _service.addRoom(hotelroom);
            return Ok(hotel);
        }

        [HttpPost("addRate")]
        public async Task<ActionResult> addRate([FromBody] RoomRate roomRate)
        {
            var rate = await _service.addRate(roomRate);
            return Ok(rate);
        }

        [HttpPost("addBlackOutDate")]
        public async Task<ActionResult>addBlackOutDate([FromBody]BlackOutDate date)
        {
            var blackOutDate = await _service.addBlackOutDate(date);
            if (blackOutDate == null)
            {
                return Ok(null);
            }
            return Ok("Date Added");

        }

        [HttpPut("UpdateHotel")]
        public async Task<IActionResult> UpdateHotel([FromBody] Hotel hotel)
        {
            var Updatehotel = await _service.UpdateHotel(hotel);
            return Ok(Updatehotel);
        }

        [HttpPut("UpdateRoom")]
        public async Task<IActionResult> UpdateRoom([FromBody] HotelRoom hotelRoom)
        {
            var UpdateRoom = await _service.UpdateRoom(hotelRoom);
            return Ok(UpdateRoom);
        }

        [HttpPut("UpdateRate")]
        public async Task<IActionResult> UpdateRate([FromBody] RoomRate roomRate)
        {
            var UpdateRate = await _service.UpdateRate(roomRate);
            return Ok(UpdateRate);
        }
        [HttpDelete("DeleteHotel/{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _service.DeleteHotel(id);
            if(hotel == null)
            {
                return NotFound();
            }
            return Ok(hotel);
        }

        [HttpDelete("DeleteRoom/{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var hotelroom = await _service.DeleteRoom(id);
            if (hotelroom == null)
            {
                return NotFound();
            }
            return Ok(hotelroom);
        }

        [HttpDelete("DeleteRate/{id}")]
        public async Task<IActionResult> DeleteRate(int id)
        {
            var roomrate = await _service.DeleteRate(id);
            if (roomrate == null)
            {
                return NotFound();
            }
            return Ok(roomrate);
        }

        [HttpDelete("DeleteBlackOutDate")]
        public async Task<ActionResult> DeleteBlackOutDate(DateTime date)
        {
            var blackOutDate = await _service.DeleteBlackOutDate(date);
            if (blackOutDate == null)
            {
                return Ok(null);
            }
            return Ok("Date Deleted");
        }
    }
}
