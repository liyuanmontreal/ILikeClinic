using ILikeClinic.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ILikeClinic.Pages.Calendar
{
    public class PatientCalendarModel : PageModel
    {
        

        private readonly ApplicationDbContext _context;

        public PatientCalendarModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }
    }
}
