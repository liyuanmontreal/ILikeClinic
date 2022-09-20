using System.ComponentModel.DataAnnotations;

namespace ILikeClinic.Model
{
    public class Availability
    {
        [Key]
        public int Id { get; set; }

        public DateTime AvailableTime { get; set; }

        public int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }


    }
}
