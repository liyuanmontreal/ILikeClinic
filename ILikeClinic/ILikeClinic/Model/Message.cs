using System.ComponentModel.DataAnnotations;

namespace ILikeClinic.Model
{
    public class Message
    {
        [Key]
        public int Id { get; set; }


        public string FromId { get; set; }

        public string ToId { get; set; }

        public string Text { get; set; }

        public string Status { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:g}", ApplyFormatInEditMode = true)]
        [Display(Name = "Time")]
        public DateTime DOB { get; set; }

    }

}
