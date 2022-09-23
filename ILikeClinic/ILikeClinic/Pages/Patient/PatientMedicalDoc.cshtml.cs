using ILikeClinic.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ILikeClinic.Pages.Patient
{
    public class PatientMedicalDocModel : PageModel
    {

        [BindProperty]
        public ILikeClinic.Model.Patient Patient { get; set; }



        [BindProperty]
        public IEnumerable<ILikeClinic.Model.MedicalHistory> MedicalHistory { get; set; }

        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PatientMedicalDocModel(ApplicationDbContext db, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
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
            }

        }

        public void OnGet()
        {
            var query = _db.MedicalHistory.AsNoTracking();
            var result = query.Where(s => s.PatientId.Equals(Patient.Id));
            if(result.Count() > 0)
            {
                MedicalHistory = result.Include(c =>c.Doctor);
            }
            else
            {
                MedicalHistory = _db.MedicalHistory;
            }
             
        }
    }
}
