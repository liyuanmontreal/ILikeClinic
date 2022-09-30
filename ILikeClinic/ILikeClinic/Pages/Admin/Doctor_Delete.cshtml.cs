using ILikeClinic.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace ILikeClinic.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class Doctor_DeleteModel : PageModel
    {

        private readonly ApplicationDbContext _db;


        public ILikeClinic.Model.Doctor Doctor { get; set; } = default!;
        public ILikeClinic.Model.DoctorItemForCalender DoctorItem { get; set; } = default!;
        private readonly UserManager<IdentityUser> _userManager;

        public Doctor_DeleteModel(ILikeClinic.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _db = context;
            _userManager = userManager;
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


        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _db.Doctor == null)
            {
                return NotFound();
            }
            var doctor = await _db.Doctor.FindAsync(id);
            var user = await _userManager.FindByIdAsync(doctor.UserId);

            if (doctor != null)
            {
                Doctor = doctor;
                _db.Doctor.Remove(Doctor);
                await _db.SaveChangesAsync();
                _ = _userManager.DeleteAsync(user);

            }
            TempData["delete"] = "Doctor Account Deleted successfully";



            //delete doctor item
            var doctoritem = await _db.DoctorItemForCalender.FindAsync(id);
          
            if (doctoritem != null)
            {
                DoctorItem = doctoritem;
                _db.DoctorItemForCalender.Remove(DoctorItem);
                await _db.SaveChangesAsync();
           
            }
         
            return RedirectToPage("./Doctor_List");
        }
    }
}

