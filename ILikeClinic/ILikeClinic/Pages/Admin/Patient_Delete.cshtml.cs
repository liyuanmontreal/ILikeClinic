using ILikeClinic.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ILikeClinic.Pages.Admin
{
    public class Patient_DeleteModel : PageModel
    {

        private readonly ApplicationDbContext _db;


        public ILikeClinic.Model.Patient Patient { get; set; } = default!;
        private readonly UserManager<IdentityUser> _userManager;

        public Patient_DeleteModel(ILikeClinic.Data.ApplicationDbContext context)
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
            if (patient == null)
            {
                return NotFound();
            }
            else
            {
                Patient = patient;
            }
            return Page();
        }


        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _db.Patient == null)
            {
                return NotFound();
            }
            var patient = await _db.Patient.FindAsync(id);
            var user = await _userManager.FindByIdAsync(Patient.UserId);

            if (patient != null)
            {
                Patient = patient;
                _db.Patient.Remove(Patient);
                await _db.SaveChangesAsync();
                _ = _userManager.DeleteAsync(user);                
             
            }
            TempData["delete"] = "Patient Account Deleted successfully";
            return RedirectToPage("./Patient_List");
        }
    }
}
