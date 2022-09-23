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

        public DateTime DateOfApp { get; set; }

        public int Weight { get; set; }

        public int Height { get; set; }

        public string BloodPressure { get; set; }

        public int Temperature { get; set; }

        public string Allergy { get; set; }

        public string HealthSituation { get; set; }

        public string MedicalDocUrl { get; set; }

        [ForeignKey("PatientId")]
        [ValidateNever]
        public virtual Patient Patient { get; set; }

        [ForeignKey("DoctorId")]
        [ValidateNever]
        public virtual Doctor Doctor { get; set; }
    }
}
