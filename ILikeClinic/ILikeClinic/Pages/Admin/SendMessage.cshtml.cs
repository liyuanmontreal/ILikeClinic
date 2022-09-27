using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ILikeClinic.Data;
using ILikeClinic.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
namespace ILikeClinic.Pages.Admin
{
    public class SendMessageModel : PageModel
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
        public SendMessageModel(ApplicationDbContext db, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _DB = db;
            _HttpContextAccessor = httpContextAccessor;
        }

         public async Task OnGet()
        {
            var items2 = await _DB.Patient.ToListAsync();
            PatientList = new SelectList(items2, "Id", "FullName");
            //Patient = _DB.Patient.Find(id);
            var items3 = await _DB.Doctor.ToListAsync();
            DoctorList = new SelectList(items3, "Id", "FullName");
        }

        public async Task<IActionResult> OnPostAsync(IFormFile file)
        {
            Message.FromId = _DB.ADMIN_ID;

            if (Doctor.Id != 0)
            {
                Message.ToId = Doctor.Id;
            }
            if (Patient.Id != 0)
            {
                Message.ToId = Patient.Id;
            }
            Message.SendTime = DateTime.Now;

            Message.Status = "SENT";
            _DB.Message.Add(Message);
            await _DB.SaveChangesAsync();

            TempData["success"] = "Message sent successfully";
            return RedirectToPage("/Index");
        }
    }
}
