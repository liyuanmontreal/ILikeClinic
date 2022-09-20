using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ILikeClinic.Model
{
    [NotMapped]
    public class ApplicationUser : IdentityUser
    {

        public virtual Doctor Doctor { get; set; }
    }
}
