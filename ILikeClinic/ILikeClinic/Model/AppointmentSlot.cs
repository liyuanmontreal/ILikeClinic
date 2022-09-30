using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ILikeClinic.Model
{
    public class AppointmentSlot
    {

        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        [JsonIgnore()]
        public Doctor Doctor { get; set; }

        [JsonPropertyName("text")]
        public string? PatientName { set; get; }

        [JsonPropertyName("patient")]
        public string? PatientId { set; get; }

        public string Status { get; set; } = "free";

        [NotMapped]
        public int Resource { get { return Doctor.Id; } }

        [NotMapped]
        public string DoctorName { get { return Doctor.FullName; } }
        
    }
}
