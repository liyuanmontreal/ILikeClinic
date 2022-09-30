using ILikeClinic.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ILikeClinic.Pages.Patient
{
    public class AppointmentSlotModel : PageModel
    {

        [BindProperty]
        public IEnumerable<ILikeClinic.Model.AppointmentSlot> Appointment { get; set; }

        [BindProperty]
        public ILikeClinic.Model.Patient Patient { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }

        public readonly ApplicationDbContext _db;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppointmentSlotModel(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
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
            }
        }

        public void OnGet()
        {
            var query = _db.Appointments.AsNoTracking();
            var patientId = Patient.Id;
            query = query.Where(a=> a.Status!="free" );
            query = query.Where(a => a.PatientId.Equals(Convert.ToString(patientId)));
            
           
            if (!string.IsNullOrEmpty(Search))
            {
                query = query.Where(s => s.Doctor.FirstName.Contains(Search) || s.Doctor.LastName.Contains(Search));
            }
            Appointment = query.Include(c => c.Doctor);

        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
            var Appointment = await _db.Appointments.FindAsync(id);
            if (Appointment == null)
            {
                return NotFound();
            }
            _db.Appointments.Remove(Appointment);
            _db.SaveChanges();

            TempData["delete"] = "Appointment deleted successfully!";
            return RedirectToPage();
        }
    }
}

