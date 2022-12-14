using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Text.Json.Serialization;

namespace ILikeClinic.Model
{
    public class Doctor
    {

        [Key]
        public int Id { get; set; }

        public string Name { get
            {
                return FirstName +" "+ LastName;
            }
        }

        
        [Required]   
        [Display(Name = "First Name")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(100, ErrorMessage = "Last name cannot be longer than 100 characters")]
        public string LastName { get; set; }

        
        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return LastName + ", " + FirstName; }
        }

        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Birth")]
        public DateTime DOB { get; set; }

       
        public Gender Gender { get; set; }


        
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; }

        
        public string ProfilePhoto { get; set; }

    
        public string Speciality { get; set; }

        
        [StringLength(400, MinimumLength = 10)]
        public string Description { get; set; }

        
        public int LicenseNumber { get; set; }

        
        [DataType(DataType.Date), Display(Name = "Hired Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime HiredDate { get; set; }

        
        public string UserId { get; set; }

        
        [ForeignKey("UserId")]
        // [ValidateNever]
        public virtual ApplicationUser User { get; set; }

        
        public virtual ICollection<AppointmentSlot>? AppointmentSlots { get; set; }
        public virtual ICollection<Appointment>? Appointments { get; set; }

    }
}
