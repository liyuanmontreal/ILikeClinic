using ILikeClinic.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ILikeClinic.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class Doctor_DetailsModel : PageModel
    {

        private readonly ApplicationDbContext _db;


        public ILikeClinic.Model.Doctor Doctor { get; set; } = default!;

        public Doctor_DetailsModel(ILikeClinic.Data.ApplicationDbContext context)
        {
            _db = context;
        }
        public async Task<IActionResult> OnGetAsync(int? id)
        
        {
            if (id == null || _db.Doctor == null)
            {
                return NotFound();
            }

            var doctor = await _db.Doctor.FirstOrDefaultAsync(m => m.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }
            else
            {
                Doctor = doctor;
            }
            return Page();
        }
    }
}

