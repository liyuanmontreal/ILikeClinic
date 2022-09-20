using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace ILikeClinic.Model
{
    [NotMapped]
    public class ApplicationUser : IdentityUser
    {

        public virtual Doctor Doctor { get; set; }

        public virtual Patient Patient { get; set; }
    }
}