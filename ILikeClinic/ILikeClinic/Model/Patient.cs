using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace ILikeClinic.Model
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name ="First Name")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name ="Last Name")]
        [StringLength(100, ErrorMessage = "Last name cannot be longer than 100 characters")]
        public string LastName { get; set; }

        public string FullName => FirstName + " " + LastName;

        [Display(Name ="Phone Number")]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        [Display(Name ="Date Of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }


        public Gender Gender { get; set; }

        public string MedicalCardNo { get; set; }

        [Display(Name ="Family Doctor")]
        public int? DoctorId { get; set; }

        /*[ForeignKey("DoctorId")]
        public virtual Doctor Doctor { get; set; }
        */
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationPatient User { get; set; }

        public virtual ICollection<Appointment>? Appointments { get; set; }

    }
}
