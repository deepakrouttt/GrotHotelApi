using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace GrotHotelApi.Models
{
    public class HotelRoom
    {
        [Required]
        [Key]
        public int HotelRoomId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string RoomPicture { get; set; }
        [Required]
        public string Description { get; set; }

        public int HotelId { get; set; }
        public ICollection<RoomRate>? RoomRates { get; set; }
    }
    public class RoomRate
    {
        [Key]
        public int RoomRateId { get; set; }
        [Required]
        public DateTime DateFrom { get; set; }
        [Required]
        public DateTime DateTo { get; set; }

        public BlackOutDate? BlackOutDate { get; set; }
        [Required]
        public decimal SingleRate { get; set; }
        [Required]
        public decimal DoubleRate { get; set; }
        [Required]
        public decimal TripleRate { get; set; }
        [Required]
        public decimal AdultRate { get; set; }
        [Required]
        public decimal childRate { get; set; }
      
        public bool IsException { get; set; }

        public bool IsExtraAdult { get; set; }

        public bool IsChildAllow { get; set; }

        public bool IsSingleEqualDouble { get; set; }

        public int HotelRoomId { get; set; }
    }
    public class BlackOutDate
    {
        [Key]
        public int Id { get; set; }
        public List<DateTime> ListDate { get; set; }

        public int RoomRateId { get; set; }
    }
}
