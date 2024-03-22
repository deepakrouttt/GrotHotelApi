using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrotHotelApi.Models
{
    public class BlackOutDate
    {
        [Key]
        public int Id { get; set; }
        public int RoomRateId { get; set; }

        public virtual ICollection<DateEntry> Dates { get; set; }


        public BlackOutDate()
        {
            Dates = new List<DateEntry>();
        }
    }

    public class DateEntry
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public int BlackOutDateId { get; set; } 

    }
    public class BlackoutData
    {
        public int Id { get; set; }
        public List<string> BlackOutDates { get; set; }
    }
}
