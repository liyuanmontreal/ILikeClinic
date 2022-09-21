using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ILikeClinic.Model
{
    public class FAQ
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Question")]
        [StringLength(100, ErrorMessage = "Question cannot be longer than 100 characters")]
        public string Question { get; set; }

        [Required]
        [Display(Name = "Answer")]
        [StringLength(200, ErrorMessage = "Answer cannot be longer than 200 characters")]
        public string Answer { get; set; }

        [Required]
        [Display(Name = "From Email")]
        [StringLength(50, ErrorMessage = "Email cannot be longer than 50 characters")]
        public string FromEmail { get; set; }

        [Required]
        [Display(Name = "Answer By")]
        [ForeignKey("AspNetUsersId")]
        public int AspNetUsersId { get; set; } 

        //[ValidateNever]
        //public AspNetUsers AspNetUsers { get; set; }

    }
}
