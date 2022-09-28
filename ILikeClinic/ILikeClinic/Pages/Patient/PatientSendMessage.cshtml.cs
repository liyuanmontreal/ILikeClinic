using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ILikeClinic.Data;
using ILikeClinic.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ILikeClinic.Pages.Patient
{
    public class PatientSendMessageModel : PageModel
    {
        public readonly ApplicationDbContext _DB;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly IWebHostEnvironment _hostEnvironment;

        [BindProperty]
        public Message Message { get; set; }

        [BindProperty]
        public Status Status { get; set; }

        [BindProperty]
        public ILikeClinic.Model.Patient Patient { get; set; }

        [BindProperty]
        public ILikeClinic.Model.Doctor Doctor { get; set; }

        public SelectList DoctorList { get; set; }

        public SelectList PatientList { get; set; }
        public PatientSendMessageModel(ApplicationDbContext db, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _DB = db;
            _HttpContextAccessor = httpContextAccessor;
            getPatient();
        }

        private void getPatient()
        {
            var userId = _HttpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var query = _DB.Patient.AsNoTracking();

            var result = query.Where(s => s.UserId.Equals(userId));
            if (result.Count() > 0)
            {
                Patient = result.First();
            }
        }


        public async Task OnGet(int? id)
        {
            var items2 = await _DB.Doctor.ToListAsync();
            DoctorList = new SelectList(items2, "Id", "FullName");
            Doctor = _DB.Doctor.Find(id);
        }

        public async Task<IActionResult> OnPostAsync(IFormFile file)
        {
            Message.FromId = Patient.Id;
            //Message.ToId = Doctor.Id;
            Message.SendTime = DateTime.Now;

            Message.Status = "SENT";
            _DB.Message.Add(Message);
            await _DB.SaveChangesAsync();

            TempData["success"] = "Message sent successfully";
            return RedirectToPage("/Index");
        }
    }
}
