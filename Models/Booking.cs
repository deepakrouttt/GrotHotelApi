﻿using Microsoft.Build.Framework;
namespace GrotHotel.Models
{
    public class Booking
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int? BookingId { get; set; }
        [Required]
        public DateTime DateFrom { get; set; }
        [Required]
        public DateTime DateTo { get; set; }
        [Required]
        public int Adult { get; set; }

        public int Children { get; set; }

        public decimal? Rate { get; set; }
    }
}
