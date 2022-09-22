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
    public class PatientEditAppointmentModel : PageModel
    {
        [BindProperty]
        public ILikeClinic.Model.Appointment Appointment { get; set; }

        [BindProperty]
        public ILikeClinic.Model.Patient Patient { get; set; }

        [BindProperty]
        public ILikeClinic.Model.Doctor Doctor { get; set; }

        public SelectList DoctorList { get; set; }

        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PatientEditAppointmentModel(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
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
        public async Task OnGet(int id)
        {
            var items = await _db.Doctor.ToListAsync();
            DoctorList = new SelectList(items, "Id", "FullName");

            Appointment = _db.Appointment.Find(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _db.Appointment.Update(Appointment);
            _db.SaveChangesAsync();

            TempData["success"] = "Appointment updated successfully";
            return RedirectToPage("./PatientAppointment");
        }
                        
    }
}
