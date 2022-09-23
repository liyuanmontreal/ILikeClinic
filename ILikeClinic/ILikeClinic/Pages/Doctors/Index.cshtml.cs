using ILikeClinic.Data;
using ILikeClinic.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace ILikeClinic.Pages.Doctors
{
    [Authorize(Roles = "Doctor")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _DB;

        public IEnumerable<Doctor> Doctors { get; set; }

        //Constructor
        public IndexModel(ApplicationDbContext db)
        {
            _DB = db;
            //if want to show every property of other table
            //_DB.Doctor.Include(u => u.Availabilities);
        }

        public void OnGet()
        {
            Doctors = _DB.Doctor;
        }
    }
}
