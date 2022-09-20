using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace ILikeClinic.Model
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }

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

        public string PhoneNumber { get; set; }

        public string ProfilePhoto { get; set; }

        public string Speciality { get; set; }

        public string Description { get; set; }

        public int LicenseNumber { get; set; }

        public DateTime HiredDate { get; set; }


        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}