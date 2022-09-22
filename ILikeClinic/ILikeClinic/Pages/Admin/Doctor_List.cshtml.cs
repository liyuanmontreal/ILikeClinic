using ILikeClinic.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ILikeClinic.Pages.Admin
{
    public class Doctor_ListModel : PageModel
    {
        private readonly ApplicationDbContext _db;


        public IList<ILikeClinic.Model.Doctor> Doctors { get; set; } = default!;

        public Doctor_ListModel(ILikeClinic.Data.ApplicationDbContext context)
        {
            _db = context;
        }

        public async Task OnGetAsync()
        {
            if (_db.Doctor != null)
            {
                Doctors = await _db.Doctor.ToListAsync();
            }


        }
    }
}
