using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ILikeClinic.Model
{
    public class Availability
    {
        [Key]
        public int Id { get; set; }

        public DateTime AvailableTime { get; set; }

        public int? DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        [ValidateNever]
        public virtual Doctor Doctor { get; set; }


    }
}
