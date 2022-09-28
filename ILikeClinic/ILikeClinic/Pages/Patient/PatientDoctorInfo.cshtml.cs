using ILikeClinic.Data;
using ILikeClinic.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ILikeClinic.Pages.Patient
{
    public class PatientDoctorInfoModel : PageModel
    {
        private readonly ApplicationDbContext _DB;
        [BindProperty]
        public ILikeClinic.Model.Doctor Doctor { get; set; }

        //Constructor
        public PatientDoctorInfoModel(ApplicationDbContext db)
        {
            _DB = db;
        }
        public void OnGet(int id)
        {
            Doctor = _DB.Doctor.Find(id);
        }
    }
}
