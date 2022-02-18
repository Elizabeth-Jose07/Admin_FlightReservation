using System;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Admin.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int UserId { get; set; }                //primary key
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name required")]
        [MaxLength(20, ErrorMessage = "Maximum 20 characters")]

        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name required")]
        [MaxLength(20, ErrorMessage = "Maximum 20 characters")]

        public  string LastName { get; set; }

        [Display(Name = "EmailID")]
        [Required(ErrorMessage = "Emailid required")]
        [MaxLength(20, ErrorMessage = "Maximum 20 characters"), MinLength(5, ErrorMessage = "Minimum 5 characters")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        
        [Required(ErrorMessage = "Password required")]
        [MaxLength(10, ErrorMessage = "Maximum 10 characters"), MinLength(8, ErrorMessage = "Minimum 8 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [NotMapped]
        [Display(Name ="Confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password does not match")]      
        [MaxLength(10, ErrorMessage = "Maximum 10 characters"), MinLength(8, ErrorMessage = "Minimum 8 characters")]

        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please specify the gender")]
        [StringLength(10, ErrorMessage = "please specify gender")]

        public string Gender { get; set; }

        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage ="Date of birth is required")]

        public DateTime DOB { get; set; }

        [Display(Name = "Phone Name")]
        [Required(ErrorMessage = "Phone number required"),RegularExpression(@"^([0-9]{10})$",ErrorMessage="Phone number not valid")]
        [StringLength(10)]
        public string Phone { get; set; }

       [Required]
        [DefaultValue(true)]
        public bool Isactive { get; set; }
       


    }
}
