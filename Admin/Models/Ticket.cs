using System;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Admin.Models
{
    [Table("Tickets")]
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }
        [Required]
        [DefaultValue(false)]
        public bool TicketStatus { get; set; }   

        [Required(ErrorMessage ="Firstname required")]
        [Display(Name ="First Name")]
        [StringLength(50,ErrorMessage ="No more than 50 characters")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "No more than 50 characters")]
        public string LastName { get; set; }

        [Required]
        [Display(Name ="Class")]
        public string Class { get; set; }

        [Required]
        [Display(Name ="Age")]
        public int Age { get; set; }

        [Required]
        [Display(Name ="Gender")]
        public string Gender { get; set; }
        [Required]
        public int SeatNumber { get; set; }

        public long BookingId { get; set; }

        [ForeignKey("BookingId")]
        public virtual Booking Booking { get; set; }    
       


    }
}
