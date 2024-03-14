using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GrotHotelApi.Data;
using GrotHotelApi.Models;

namespace GrotHotelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelApiController : ControllerBase
    {
        private readonly GrotHotelApiDbContext _context;

        public HotelApiController(GrotHotelApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotels()
        {
            var Hotels = await _context.Hotels.Include(m => m.HotelRooms).ToListAsync();
            return Hotels;
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
            var hotel = await _context.Hotels.Include(m => m.HotelRooms).FirstOrDefaultAsync(m => m.HotelId == id);

            if (hotel == null)
            {
                return NotFound();
            }

            return hotel;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel([FromBody] Hotel hotel)
        {
            var Updatehotel = await _context.Hotels.Include(m => m.HotelRooms).FirstOrDefaultAsync(m => m.HotelId == hotel.HotelId);
            Updatehotel.HotelName = hotel.HotelName;
            Updatehotel.HotelImage = hotel.HotelImage;
            Updatehotel.Address = hotel.Address;
            Updatehotel.ChildAgeRange = hotel.ChildAgeRange;
            Updatehotel.Rating = hotel.Rating;

            _context.Hotels.Update(Updatehotel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel([FromBody] Hotel hotel)
        {
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHotel", new { id = hotel.HotelId }, hotel);
        }

        [HttpPost("PostRoom")]
        public async Task<ActionResult<Hotel>> PostRoom([FromBody] HotelRoom hotelroom)
        {
            var hotel = await _context.Hotels.Include(a => a.HotelRooms).SingleOrDefaultAsync(m => m.HotelId == hotelroom.HotelId);
            hotel.HotelRooms.Add(hotelroom);
            await _context.SaveChangesAsync();

            return hotel;
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HotelExists(int id)
        {
            return _context.Hotels.Any(e => e.HotelId == id);
        }
    }
}
