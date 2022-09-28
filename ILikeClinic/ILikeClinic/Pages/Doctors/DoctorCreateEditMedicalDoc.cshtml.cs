using ILikeClinic.Data;
using ILikeClinic.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ILikeClinic.Pages.Doctors
{
    public class DoctorCreateEditMedicalDocModel : PageModel
    {
        [BindProperty]
        public ILikeClinic.Model.Patient Patient { get; set; }

        [BindProperty]
        public ILikeClinic.Model.Doctor Doctor { get; set; }
        [BindProperty]
        public ILikeClinic.Model.MedicalHistory MedicalHistory { get; set; }

        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _hostEnvironment;

        public DoctorCreateEditMedicalDocModel(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            getDoctor();
            _hostEnvironment = hostEnvironment; 
        }

        private void getDoctor()
        {
            if (Doctor == null)
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var query = _db.Doctor.AsNoTracking();
                var result = query.Where(s => s.UserId.Equals(userId));
                if (result.Count() > 0)
                {
                    Doctor = result.First();
                }
            }
        }
        public void OnGet(int? id)
        {
            Patient = _db.Patient.Find(id);
        }

        public async Task<IActionResult> OnPost(IFormFile file, int patientID)
        {
            MedicalHistory.PatientId = Patient.Id;
            MedicalHistory.DoctorId = Doctor.Id;
            MedicalHistory.DateOfApp = DateTime.Today;

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
                MedicalHistory.MedicalDocUrl = @"\file\" + fileName + extension;
            }


            _db.MedicalHistory.Add(MedicalHistory);
            _db.SaveChanges();

            TempData["success"] = "A new medical document is created successfully";

            return RedirectToPage("DoctorMedicalDoc", new { id = patientID });
        }
    }
}
