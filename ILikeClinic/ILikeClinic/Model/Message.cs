using System.ComponentModel.DataAnnotations;

namespace ILikeClinic.Model
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        public int? FromId { get; set; }

        public int? ToId { get; set; }

        public string Text { get; set; }

        public string Status { get; set; }

        public DateTime SendTime { get; set; }

    }
}
