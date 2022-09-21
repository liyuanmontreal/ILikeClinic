using ILikeClinic.Data;
using ILikeClinic.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ILikeClinic.Pages.Doctors
{
    public class UpsertModel : PageModel
    {
        private readonly ApplicationDbContext _DB;

        public Doctor Doctor { get; set; }

        public UpsertModel(ApplicationDbContext db)
        {
            _DB = db;
            Doctor = new Doctor();
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            return Page();
        }
    }
}
