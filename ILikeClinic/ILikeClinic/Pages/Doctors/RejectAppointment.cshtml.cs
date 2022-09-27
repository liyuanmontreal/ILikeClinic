using ILikeClinic.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;



namespace ILikeClinic.Pages.Doctors
{
    public class RejectAppointmentModel : PageModel
    {
        private readonly ApplicationDbContext _db;


        public ILikeClinic.Model.Appointment Appointment { get; set; } = default!;


        public RejectAppointmentModel(ILikeClinic.Data.ApplicationDbContext context)
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


        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _db.Appointment == null)
            {
                return NotFound();
            }
            var appointment = await _db.Appointment.FindAsync(id);


            if (appointment != null)
            {
                Appointment = appointment;
                _db.Appointment.Remove(Appointment);
                await _db.SaveChangesAsync();


            }
            TempData["delete"] = "Appointment Deleted successfully";
            return RedirectToPage("AppointmentToDoctor");
        }
    }
}

