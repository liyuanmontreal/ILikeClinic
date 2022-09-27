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
    public class AppointmentToDoctorModel : PageModel
    {
        private readonly ApplicationDbContext _DB;
        private readonly IHttpContextAccessor _HttpContextAccessor;

        public IEnumerable<Appointment> Appointments { get; set; }

        
        [BindProperty]
        public ILikeClinic.Model.Doctor Doctor { get; set; }

        [BindProperty]
        public IList<ILikeClinic.Model.Patient> Patients { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchNameString { get; set; }

        //Constructor
        public AppointmentToDoctorModel(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _DB = db;
            _HttpContextAccessor = httpContextAccessor;
            getDoctor();
            //if want to show every property of other table
            //_DB.Doctor.Include(u => u.Availabilities);
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


        public void OnGet()
        {
            var appointmentsToD = _DB.Appointment.Where(a => a.Doctor.Id == Doctor.Id).AsNoTracking();
            

                //lambda 
                if (!string.IsNullOrEmpty(SearchNameString))
                {
                    appointmentsToD = _DB.Appointment.Where(s => s.Patient.FirstName.Contains(SearchNameString) || s.Patient.LastName.Contains(SearchNameString));

                }

                Appointments = appointmentsToD.Include(c => c.Patient);
        }


        //public async Task<IActionResult> OnPostDelete(int id)
        //{
        //    if (id == null || _DB.Appointment == null)
        //    {
        //        return NotFound();
        //    }
        //    var Appointment = await _DB.Appointment.FindAsync(id);
        //    if (Appointment != null)
        //    {
        //        _DB.Appointment.Remove(Appointment);
        //        _DB.SaveChanges();

        //        TempData["delete"] = "Appointment deleted successfully!";
               
        //    }

        //    return Page();
        //}
    }
}
