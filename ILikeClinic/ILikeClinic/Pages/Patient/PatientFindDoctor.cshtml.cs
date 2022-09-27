using ILikeClinic.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ILikeClinic.Pages.Patient
{
    public class PatientFindDoctorModel : PageModel
    {
        [BindProperty]
        public ILikeClinic.Model.Patient Patient { get; set; }


        [BindProperty]
        public IEnumerable<Model.Doctor> Doctor { get; set; }
        [BindProperty]
        public ILikeClinic.Model.Doctor doctor { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }

        public readonly ApplicationDbContext _db;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public PatientFindDoctorModel(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            getMyDoctor();
        }

        private void getMyDoctor()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var query = _db.Patient.AsNoTracking();

            var result = query.Where(s => s.UserId.Equals(userId));

            if (result.Count() > 0)
            {
                Patient = result.First();
            }

            doctor = _db.Doctor.Find(Patient.DoctorId);
            
        }
        public void OnGet()
        {

           Doctor = _db.Doctor;
        }
    }
}
