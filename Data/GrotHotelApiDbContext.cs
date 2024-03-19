using GrotHotel.Models;
using GrotHotelApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GrotHotelApi.Data
{
    public class GrotHotelApiDbContext : DbContext
    {
        public GrotHotelApiDbContext(DbContextOptions<GrotHotelApiDbContext> options): base(options)
        {
        }
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<HotelRoom> HotelRooms { get; set; }
        public virtual DbSet<RoomRate> RoomRates { get; set; }
        public virtual DbSet<BlackOutDate> BlackOutDates { get; set; }
        public virtual DbSet<DateEntry> DateEntries { get; set; }
        public virtual DbSet<User>Users { get; set; }
        public virtual DbSet<Booking>Bookings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
