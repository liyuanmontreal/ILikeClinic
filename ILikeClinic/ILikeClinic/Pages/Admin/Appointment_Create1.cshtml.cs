using ILikeClinic.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ILikeClinic.Pages.Admin
{
    public class Appointment_CreateModel1 : PageModel
    {
        [BindProperty]
        public ILikeClinic.Model.Appointment Appointment { get; set; }

     

        //public ILikeClinic.Model.Patient Patient { get; set; }      
        //public ILikeClinic.Model.Doctor Doctor { get; set; }

        public readonly ApplicationDbContext _db;
        public Appointment_CreateModel1(ApplicationDbContext context )
        {
            _db = context;
          
        }

        public void OnGet()
        {
            //ViewData["DoctorID"] = new SelectList(_db.Doctor, "ID", "FullName");
            //ViewData["PatientID"] = new SelectList(_db.Patient, "ID", "FullName");
        }

        public async Task<IActionResult> OnPostAsync()
        {


            var doctor = await _db.Doctor.FindAsync(Appointment.DoctorId);
            Appointment.Doctor = doctor;
            var patient = await _db.Patient.FindAsync(Appointment.PatientId);
            Appointment.Patient = patient;
            _db.Appointment.Add(Appointment);
            _db.SaveChanges();

            TempData["success"] = "Appointment  created successfully";


            return RedirectToPage("Appointment_List");
        }

    }
}

