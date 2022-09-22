using ILikeClinic.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ILikeClinic.Pages.Admin
{
   
    public class Appointment_DetailsModel : PageModel
    {
        public readonly ApplicationDbContext _db;
        public ILikeClinic.Model.Appointment Appointment { get; set; } = default!;

        public Appointment_DetailsModel(ILikeClinic.Data.ApplicationDbContext context)
        {
            _db = context;

        }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _db.Appointment == null)
            {
                return NotFound();
            }

         
            var appointment = await _db.Appointment.FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }
            else
            {
                Appointment = appointment;
                var doctor = await _db.Doctor.FindAsync(Appointment.DoctorId);
                Appointment.Doctor = doctor;
                var patient = await _db.Patient.FindAsync(Appointment.PatientId);
                Appointment.Patient = patient;
            }
            return Page();
        }
    }
}
