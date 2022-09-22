using ILikeClinic.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ILikeClinic.Pages.Admin
{
    public class Patient_DetailModel : PageModel
    {

        private readonly ApplicationDbContext _db;


        public ILikeClinic.Model.Patient Patient { get; set; } = default!;

        public Patient_DetailModel(ILikeClinic.Data.ApplicationDbContext context)
        {
            _db = context;
        }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _db.Patient== null)
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
    }
}
