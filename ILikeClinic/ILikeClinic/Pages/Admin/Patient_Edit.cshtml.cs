using ILikeClinic.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ILikeClinic.Pages.Admin
{
    public class Patient_EditModel : PageModel
    {

        private readonly ApplicationDbContext _db;

        [BindProperty]
        public ILikeClinic.Model.Patient Patient { get; set; } = default!;

        public Patient_EditModel(ILikeClinic.Data.ApplicationDbContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            //_db.Attach(Patient).State = EntityState.Modified;


            _db.Patient.Update(Patient);
            _db.SaveChanges();
            TempData["success"] = "Patient profile updated successfully";

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(Patient.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            TempData["success"] = "Patient Profile updated successfully";

            return RedirectToPage("./Patient_List");
        }

        private bool PatientExists(int id)
        {
            return (_db.Patient?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
