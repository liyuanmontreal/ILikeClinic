using ILikeClinic.Data;
using ILikeClinic.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ILikeClinic.Pages.Patient
{
    public class AddMyFileModel : PageModel
    {
        [BindProperty]
        public ILikeClinic.Model.Patient Patient { get; set; }
        [BindProperty]
        public ILikeClinic.Model.Doctor doctor { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }

        [BindProperty]
        public Gender Gender { get; set; }

        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public AddMyFileModel(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            getPatient();
        }
        public async Task OnGet()
        {

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
        public async Task<IActionResult> OnPost()
        {
            Patient.UserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (Patient.Id != 0)
            {
                _db.Patient.Update(Patient);
                _db.SaveChanges();
                TempData["success"] = "Your profile updated successfully";
            }
            else
            {
                _db.Patient.Add(Patient);
                _db.SaveChanges();

                TempData["success"] = "Save your file successfully";
            }

            return RedirectToPage("PatientHome");
        }
    }
}
