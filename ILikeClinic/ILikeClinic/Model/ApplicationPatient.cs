using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace ILikeClinic.Model
{
    [NotMapped]
    public class ApplicationPatient : IdentityUser
    {

        public virtual Patient Patient { get; set; }

    }
}
