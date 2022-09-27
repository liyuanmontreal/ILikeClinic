using ILikeClinic.Data;
using ILikeClinic.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace ILikeClinic.Pages.Doctors
{
    [Authorize(Roles = "Doctor")]
    [BindProperties]
    public class AppointmentToDoctorModel : PageModel
    {
        private readonly ApplicationDbContext _DB;

        public IEnumerable<Appointment> Appointments { get; set; }

        public Doctor Doctor { get; set; }

        public IList<ILikeClinic.Model.Patient> Patients { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchNameString { get; set; }

        //Constructor
        public AppointmentToDoctorModel(ApplicationDbContext db)
        {
            _DB = db;
            //if want to show every property of other table
            //_DB.Doctor.Include(u => u.Availabilities);
        }

        public void OnGet()
        {
          
        }
    }
}
