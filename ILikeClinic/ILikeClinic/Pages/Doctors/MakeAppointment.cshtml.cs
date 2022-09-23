using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ILikeClinic.Data;
using ILikeClinic.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ILikeClinic.Pages.Doctors
{
    public class MakeAppointmentModel : PageModel
    {

        [BindProperty]
        public Appointment Appointment { get; set; }



        [BindProperty]
        public Status Status { get; set; }

        public readonly ApplicationDbContext _DB;
        
        private readonly IWebHostEnvironment _hostEnvironment;


        [BindProperty]
        public ILikeClinic.Model.Patient Patient { get; set; }

        [BindProperty]
        public ILikeClinic.Model.Doctor Doctor { get; set; }

        public SelectList DoctorList { get; set; }

        public SelectList PatientList { get; set; }
        public MakeAppointmentModel(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _DB = db;

        }


        public async Task OnGet(int? id)
        {
            var items1 = await _DB.Doctor.ToListAsync();
            DoctorList = new SelectList(items1, "Id", "FullName");
            Doctor = _DB.Doctor.Find(id);

            var items2 = await _DB.Patient.ToListAsync();
            PatientList = new SelectList(items2, "Id", "FullName");
            Patient = _DB.Patient.Find(id);
        }

        public async Task<IActionResult> OnPostAsync(IFormFile file)
        {

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
            _DB.Appointment.Add(Appointment);
            await _DB.SaveChangesAsync();

            TempData["success"] = "Appointment created successfully";
            return RedirectToPage("Appointment_List");


        }
    }
}
