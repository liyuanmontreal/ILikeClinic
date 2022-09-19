using ILikeClinic.Data;
using ILikeClinic.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ILikeClinic.Pages.Doctors
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _DB;

        public IEnumerable<Doctor> Doctors { get; set; }

        //Constructor
        public IndexModel(ApplicationDbContext db)
        {
            _DB = db;
        }

        public void OnGet()
        {
            Doctors = _DB.Doctor;
        }
    }
}
