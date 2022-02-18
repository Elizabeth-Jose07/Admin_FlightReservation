using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;

namespace Admin.Models
{
    [Table("FlightInfos")]
    public class FlightInfo
    {

        public FlightInfo()
        {
            Bookings = new HashSet<Booking>();
            IsActive = true;
        }

        [Key]
        public int FlightId { get; set; }                 //primary key

     
        
        //[Remote("FlightDetailsExists", "FlightDetailsController",ErrorMessage ="Flight id does not exist")]
        public int FlightNumber { get; set; }      //foreign key
       

        [Required(ErrorMessage ="source City Required")]
        [Display(Name ="Source")]
        [StringLength(40, ErrorMessage ="maximum 40 characters")]
        public string Source { get; set;}                  //source city


        [Required(ErrorMessage = "Destination City Required")]
        [Display(Name = "Destination")]
        [StringLength(40, ErrorMessage = "maximum 40 characters")]
        public string Destination { get; set; }                       //destination city


        [Required(ErrorMessage = "Departure date  Required")]
        [Display(Name = "Departure Date and time")]
        [DataType(DataType.DateTime)]                                   //departure date and time
       
        public DateTime  DepartureDate{ get; set; }


        [Required(ErrorMessage ="Arrival date required")]
        [Display(Name = " Arrival Date and time")]
        [DataType(DataType.DateTime)]

        public DateTime ArrivalDate { get; set; }               //arrival date and time

        //[Required]
        //public DepartureTime { get; set; }

        //[Required]
        //public Time ArrivalTime { get; set; }

        [Display(Name = "Buisness class seats ")] 
                                            //buisness calss saets in number
        public int  Bseats { get; set; }

        [Display(Name ="Economy class seats")]              //economy class seats
        public int Eseats { get; set; }

        [Required]

        [DefaultValue(true)]
        public bool IsActive { get; set; }      //is active


        [ForeignKey("FlightNumber")]
        public virtual FlightDetail Flight { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
