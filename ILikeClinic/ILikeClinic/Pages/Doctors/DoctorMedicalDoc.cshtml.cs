using ILikeClinic.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IWebHostEnvironment;

namespace ILikeClinic.Pages.Doctors
{
    public class DoctorMedicalDocModel : PageModel
    {

        [BindProperty]
        public ILikeClinic.Model.Patient Patient { get; set; }
        private IWebHostEnvironment _env;


        [BindProperty]
        public IEnumerable<ILikeClinic.Model.MedicalHistory> MedicalHistory { get; set; }


        private readonly ApplicationDbContext _db;


        public DoctorMedicalDocModel(ApplicationDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }


        public void OnGet(int id)
        {
            Patient = _db.Patient.Find(id);
            var query = _db.MedicalHistory.AsNoTracking();
            var result = query.Where(s => s.PatientId.Equals(id));
            if (result.Count() > 0)
            {
                MedicalHistory = result.Include(c => c.Doctor).Include(c => c.Patient);
            }
            else
            {
               // MedicalHistory = _db.MedicalHistory;
            }

        }
        public async Task<IActionResult> OnPostDelete(int id, int patientID)
        {
            var medical = await _db.MedicalHistory.FindAsync(id);
            if (medical == null)
            {
                return NotFound();
            }
            _db.MedicalHistory.Remove(medical);
            _db.SaveChanges();

            TempData["delete"] = "Medical Document deleted successfully!";
            return RedirectToPage("DoctorMedicalDoc", new { id = patientID});
        }
        public ActionResult OnPostDownloadFile(int id, int weight, String allergy, String patientFullName, String dateOfApp, String doctorFullName, int Height, String bloodPressure, int Temp, String healthSituation)
        {
            var fileName = Guid.NewGuid().ToString() + ".txt";
            var path = Path.Combine(_env.WebRootPath, @"DownloadableFiles\" + fileName);
            using (FileStream fs = System.IO.File.Create(path))
            {
                byte[] content = new System.Text.UTF8Encoding(true).GetBytes("Patient Medical Document" + "\r\n"
                    + "\r\n"
                    + "Doctor: Dr. " + doctorFullName +"\r\n"
                    +"Weight: " + weight + " kg" + "\r\n"
                    + "Height: " + Height + " cm"+ "\r\n"
                    + "Blood Pressure: " + bloodPressure + "\r\n"
                    + "Temperature: " + Temp + " ° C" + "\r\n"
                    +"Allergy: " + allergy + "\r\n"
                    + "Health Situation: " + healthSituation + "\r\n"
                    + "Date: " + dateOfApp);
                fs.Write(content, 0, content.Length);
            }
            var fsDownload = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None, 4096, FileOptions.DeleteOnClose);

            return File(fsDownload, System.Net.Mime.MediaTypeNames.Application.Octet, patientFullName +"-"+ dateOfApp + ".txt");
        }
    }
}
