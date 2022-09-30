using ILikeClinic.Data;
using ILikeClinic.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;

namespace ILikeClinic.Pages.Patient
{
    public class PrintPageModel : PageModel
    {
        [BindProperty]        
        public ILikeClinic.Model.MedicalHistory MedicalHistory { get; set; }

        private readonly ApplicationDbContext _db;

        public PrintPageModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task OnGet(int id)
        {
            var query = _db.MedicalHistory.Include(c =>c.Doctor);
            var result = query.Where(s => s.Id.Equals(id));
            if (result.Count() > 0)
            {
                MedicalHistory = result.First();        
            }
            

        }
    }
}
