using ILikeClinic.Data;
using ILikeClinic.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ILikeClinic.Pages.Patient
{
    [Authorize(Roles = "Patient")]
    public class PatientHomeModel : PageModel
    {
        [BindProperty]
        public ILikeClinic.Model.Patient Patient { get; set; }

        public ILikeClinic.Model.Doctor doctor { get; set; }

        [BindProperty]
        public IdentityUser IdentityUser { get; set; }

        public readonly ApplicationDbContext _db;

        private readonly IHttpContextAccessor _httpContextAccessor;


        public PatientHomeModel(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            getPatient();
        }

        private void getPatient()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var query = _db.Patient.AsNoTracking();
            var result = query.Where(s => s.UserId.Equals(userId));
            if (result.Count() > 0)
            {
                Patient = result.First();
                doctor = _db.Doctor.Find(Patient.DoctorId);

            }
        }

        
        public IActionResult OnGet()
        {
            if (Patient == null)
                return RedirectToPage("AddMyFile");
            return Page();
        }
    }
}
