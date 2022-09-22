using ILikeClinic.Data;
using ILikeClinic.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace ILikeClinic.Pages.Doctors
{
    [Authorize(Roles = "Doctor")]
    public class ProfileDetailModel : PageModel
    {
        private readonly ApplicationDbContext _DB;
        private readonly IHttpContextAccessor _HttpContextAccessor;

        public IEnumerable<Doctor> Doctors { get; set; }

        public Doctor Doctor { get; set; }

        //Constructor
        public ProfileDetailModel(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _DB = db;
            _HttpContextAccessor = httpContextAccessor;
            getDoctor();
            //if want to show every property of other table
            //_DB.Doctor.Include(u => u.Availabilities);
        }

        public void OnGet()
        {
            
        }

        private void getDoctor()
        {
            var userId = _HttpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var query = _DB.Doctor.AsNoTracking();

            var result = query.Where(s => s.UserId.Equals(userId));
            if (result.Count() > 0)
            {
                Doctor = result.First();
            }
        }
    }
}
