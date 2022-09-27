using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace ILikeClinic.Model
{
    public class MedicalHistory
    {
        [Key]
        public int Id { get; set; }

        public int? PatientId { get; set; }

        public int? DoctorId { get; set; }

        [Display(Name ="Date of Appointment")]
        public DateTime DateOfApp { get; set; }

        public int Weight { get; set; }

        public int Height { get; set; }

        [Display(Name ="Blood Pressure")]
        public string BloodPressure { get; set; }

        public int Temperature { get; set; }

        public string Allergy { get; set; }

        [Display(Name ="Health Situation")]
        public string HealthSituation { get; set; }

        [Display(Name ="Medical Document")]
        public string MedicalDocUrl { get; set; }

        [ForeignKey("PatientId")]
        [ValidateNever]
        public virtual Patient Patient { get; set; }

        [ForeignKey("DoctorId")]
        [ValidateNever]
        public virtual Doctor Doctor { get; set; }
    }
}
