using ILikeClinic.Data;
using ILikeClinic.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;
namespace ILikeClinic.Pages.Patient
{
    public class AnswerSurveyModel : PageModel
    {
        [BindProperty]
        public Survey Survey { get; set; }

        [BindProperty]
        public Appointment Appointment { get; set; }

        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AnswerSurveyModel(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            //getPatient();
        }
/*
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
*/
        public async Task OnGet(int? id)
        {
            /*
            var items = await _db.Doctor.ToListAsync();
            DoctorList = new SelectList(items, "Id", "FullName");

            Appointment = _db.Appointment.Find(id);
            */
            //Survey.AppointmentId = id;
            Appointment = _db.Appointment.Find(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Survey.AppointmentId = Appointment.Id;
            _db.Survey.Add(Survey); 
            _db.SaveChangesAsync();

            TempData["success"] = "Thank you for filling our survey";
            return RedirectToPage("./PatientAppointment");
        }
    }
}
