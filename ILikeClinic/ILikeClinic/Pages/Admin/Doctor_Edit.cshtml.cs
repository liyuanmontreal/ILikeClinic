using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using ILikeClinic.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ILikeClinic.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class Doctor_EditModel : PageModel
    {

        private readonly ApplicationDbContext _db;

        [BindProperty]
        public ILikeClinic.Model.Doctor Doctor { get; set; } = default!;

        public Doctor_EditModel(ILikeClinic.Data.ApplicationDbContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            //_db.Attach(Patient).State = EntityState.Modified;


            _db.Doctor.Update(Doctor);
            _db.SaveChanges();
            TempData["success"] = "Doctor profile updated successfully";

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(Doctor.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            TempData["success"] = "Doctor Profile updated successfully";

            return RedirectToPage("./Doctor_List");
        }

        private bool DoctorExists(int id)
        {
            return (_db.Doctor?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

