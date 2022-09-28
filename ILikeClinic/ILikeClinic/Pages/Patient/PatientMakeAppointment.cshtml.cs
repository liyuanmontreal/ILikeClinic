using ILikeClinic.Data;
using ILikeClinic.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace ILikeClinic.Pages.Patient
{
    public class PatientMakeAppointmentModel : PageModel
    {

        [BindProperty]
        public ILikeClinic.Model.Patient Patient { get; set; }

        [BindProperty]
        public ILikeClinic.Model.Doctor Doctor { get; set; }

        [BindProperty]
        public ILikeClinic.Model.Appointment Appointment { get; set; }

        public SelectList DoctorList { get; set; }

        [BindProperty]
        public Status Status { get; set; }

        public readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PatientMakeAppointmentModel(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _hostEnvironment = hostEnvironment;
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

        public async Task<IActionResult> OnGet(int? id)
        {
            var items = await _db.Doctor.ToListAsync();
            DoctorList = new SelectList(items, "Id", "FullName");
            if (id == null || id == 0)
            {

            }
            else
            {
                Doctor = _db.Doctor.Find(id);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile file)
        {
            Appointment.PatientId = Patient.Id;

            if (file != null)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                var uploads = Path.Combine(wwwRootPath, @"file");
                var extension = Path.GetExtension(file.FileName);
                string fileName = Guid.NewGuid().ToString();


                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    file.CopyTo(fileStreams);
                }
                Appointment.FileUrl = @"\file\" + fileName + extension;
            }

            Appointment.Status = 0;
            if(Doctor.Id != 0)
            {
                Appointment.DoctorId = Doctor.Id;
            }
            _db.Appointment.Add(Appointment);
            await _db.SaveChangesAsync();

            TempData["success"] = "Appointment created successfully";
            return RedirectToPage("PatientAppointment");


        }
    }
}
