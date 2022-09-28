using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ILikeClinic.Data;
using ILikeClinic.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ILikeClinic.Pages.Doctors
{
    public class DoctorModifyAppointmentModel : PageModel
    {
        [BindProperty]
        public Appointment Appointment { get; set; }

        [BindProperty]
        public Status Status { get; set; }

        public readonly ApplicationDbContext _DB;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly IWebHostEnvironment _hostEnvironment;


        [BindProperty]
        public ILikeClinic.Model.Patient Patient { get; set; }

        [BindProperty]
        public ILikeClinic.Model.Doctor Doctor { get; set; }

      

        public SelectList DoctorList { get; set; }

        public SelectList PatientList { get; set; }

        public DoctorModifyAppointmentModel(ApplicationDbContext db, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _DB = db;
            _HttpContextAccessor = httpContextAccessor;
            getDoctor();
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


        public async Task OnGet(int? id)
        {


            var items2 = await _DB.Patient.ToListAsync();
            PatientList = new SelectList(items2, "Id", "FullName");
            Appointment = _DB.Appointment.Find(id);

            Patient = _DB.Patient.FirstOrDefault(p => p.Id == Appointment.PatientId);
           
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _DB.Appointment.Update(Appointment);
            _DB.SaveChangesAsync();

            TempData["success"] = "Appointment updated successfully";
            return RedirectToPage("AppointmentToDoctor");
        }

    }
}
