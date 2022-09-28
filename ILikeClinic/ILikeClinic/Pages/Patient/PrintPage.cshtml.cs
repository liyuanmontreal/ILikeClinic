using ILikeClinic.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ILikeClinic.Pages.Patient
{
    public class PrintPageModel : PageModel
    {
        [BindProperty]
        public Model.MedicalHistory MedicalHistory { get; set; }

        private readonly ApplicationDbContext _db;

        public PrintPageModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task OnGet(int id)
        {
            MedicalHistory = _db.MedicalHistory.Find(id);

        }
    }
}
