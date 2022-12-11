using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Models
{
    [Table("Bookings")]
    public class Booking
    {
        public Booking()
        {
            TranStatus = true;
        }

        [Key]
        public long BookingId { get; set; }
        [Required]
        public string NoOfSeats { get; set;}

        [Required]
        [Display(Name ="Amount payable")]
        public double Amount { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }
        public long CardNo { get; set; }
        public DateTime Validity { get; set; }
        public int Cvv { get; set; }

       
        public bool TranStatus { get; set; } 

        public string Remarks { get; set; }

        
        public int UserId { get; set; }
        public int FlightId { get; set; }

        [ForeignKey("FlightId")]
        public virtual FlightInfo Flight { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

    }
}
