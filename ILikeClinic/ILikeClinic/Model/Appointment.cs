
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ILikeClinic.Model
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime AppStartAt { get; set; }

        public DateTime AppTime { get; set; }

        public Status Status { get; set; }

        

        

        public string Reason { get; set; }

        public string? FileUrl { get; set; }


        [ForeignKey("PatientId")]
        [ValidateNever]
        public virtual Patient? Patient { get; set; }
        public int? PatientId { get; set; }

        [ForeignKey("DoctorId")]
        [ValidateNever]
        public virtual Doctor? Doctor { get; set; }
        public int? DoctorId { get; set; }
    }
}
