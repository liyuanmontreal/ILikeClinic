using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ILikeClinic.Model
{
    public class Survey
    {
        [Key]
        public int Id { get; set; }

        public string Answer1 { get; set; }

        public string Answer2 { get; set; }

        public string Answer3 { get; set; }

        public string Answer4 { get; set; }

        public string Answer5 { get; set; }

        [ForeignKey("AppointmentId")]
        [ValidateNever]
        public virtual Appointment? Appointment { get; set; }
        public int? AppointmentId { get; set; }
    }
}
