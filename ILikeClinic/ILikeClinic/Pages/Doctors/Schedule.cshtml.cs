using ILikeClinic.Data;
using ILikeClinic.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Data;
using System.Security.Claims;

namespace ILikeClinic.Pages.Doctors
{
    [Authorize(Roles = "Doctor")]
    [BindProperties]
    public class ScheduleModel : PageModel
    {
        private readonly ApplicationDbContext _DB;
     
        private readonly IHttpContextAccessor _HttpContextAccessor;

        public Doctor Doctor { get; set; }
        public Availability Availability { get; set; }

        public ScheduleModel(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _DB = db;
            _HttpContextAccessor = httpContextAccessor;
            getDoctor();
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


        public IActionResult OnGetFindAllEvent()
        {
            var events = _DB.Availability.Select(a => new
            {

            }).ToList();
            return new JsonResult(events);
        }
    }
}
