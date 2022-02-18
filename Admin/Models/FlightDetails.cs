 using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel;

namespace Admin.Models
{
    [Table("FlightDetails")]
    public class FlightDetail
    {
        public FlightDetail()
        {
            FlightInfos = new HashSet<FlightInfo>();
            Isactive = true;
        }

        [Key]

        public int FlightNumber { get; set; }

        [Required(ErrorMessage ="Flight Name")]
        [Display (Name ="Flight Name")]
        public string FlightName { get; set; }

        [Required]
        [Display(Name ="Flight Model")]

        public string FlightModel { get; set; }

        [Required]
        [DefaultValue(true)]
        public bool Isactive { get; set; }
        public virtual ICollection<FlightInfo> FlightInfos { get; set; }
    }
}
