using ILikeClinic.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ILikeClinic.Pages.Patient
{
    public class PatientMedicalDocModel : PageModel
    {

        [BindProperty]
        public ILikeClinic.Model.Patient Patient { get; set; }



        [BindProperty]
        public IEnumerable<ILikeClinic.Model.MedicalHistory> MedicalHistory { get; set; }

        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IWebHostEnvironment _env;

        public PatientMedicalDocModel(ApplicationDbContext db, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _env = env;
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

        public void OnGet()
        {
            var query = _db.MedicalHistory.AsNoTracking();
            var result = query.Where(s => s.PatientId.Equals(Patient.Id));
            if(result.Count() > 0)
            {
                MedicalHistory = result.Include(c =>c.Doctor).Include(c => c.Patient);
            }
            else
            {
                //MedicalHistory = _db.MedicalHistory;
            }
             
        }

        public ActionResult OnPostDownloadFile(int id, int weight, String allergy, String patientFullName, String dateOfApp, String doctorFullName, int Height, String bloodPressure, int Temp, String healthSituation)
        {
            var fileName = Guid.NewGuid().ToString() + ".txt";
            var path = Path.Combine(_env.WebRootPath, @"DownloadableFiles\" + fileName);
            using (FileStream fs = System.IO.File.Create(path))
            {
                byte[] content = new System.Text.UTF8Encoding(true).GetBytes("Patient Medical Document" + "\r\n"
                    + "\r\n"
                    + "Doctor: Dr. " + doctorFullName + "\r\n"
                    + "Weight: " + weight + " kg" + "\r\n"
                    + "Height: " + Height + " cm" + "\r\n"
                    + "Blood Pressure: " + bloodPressure + "\r\n"
                    + "Temperature: " + Temp + " ° C" + "\r\n"
                    + "Allergy: " + allergy + "\r\n"
                    + "Health Situation: " + healthSituation + "\r\n"
                    + "Date: " + dateOfApp);
                fs.Write(content, 0, content.Length);
            }
            var fsDownload = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None, 4096, FileOptions.DeleteOnClose);

            return File(fsDownload, System.Net.Mime.MediaTypeNames.Application.Octet, patientFullName + "-" + dateOfApp + ".txt");
        }
    }
}
