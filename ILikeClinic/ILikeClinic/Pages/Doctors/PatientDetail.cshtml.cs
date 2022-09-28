using ILikeClinic.Data;
using ILikeClinic.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace ILikeClinic.Pages.Doctors
{
    [Authorize(Roles = "Doctor")]
    public class PatientDetailModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public ILikeClinic.Model.Patient Patient { get; set; }

        [BindProperty]
        public ILikeClinic.Model.Doctor Doctor { get; set; }

        public PatientDetailModel(ILikeClinic.Data.ApplicationDbContext context)
        {
            _db = context;
        }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _db.Patient == null)
            {
                return NotFound();
            }

            var patient = await _db.Patient.FirstOrDefaultAsync(m => m.Id == id);
            var doctor = await _db.Doctor.FirstOrDefaultAsync(m => m.Id == patient.DoctorId);
            if (patient == null)
            {
                return NotFound();
            }
            else
            {
                Patient = patient;
                Doctor = doctor;
            }
            return Page();
        }
    }
}
