using ILikeClinic.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace ILikeClinic.Pages.Doctors
{
    [Authorize(Roles = "Doctor")]
    public class PatientListModel : PageModel
    {
        private readonly ApplicationDbContext _DB;



        public IList<ILikeClinic.Model.Patient> Patients { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchNameString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchPhoneString { get; set; }

        public PatientListModel(ApplicationDbContext db)
        {
            _DB = db;
        }

        public async Task OnGetAsync()
        {
            if (_DB.Patient != null)
            {
                // using system.linq
                var patients = from a in _DB.Patient
                               select a;

                //lambda 
                if (!string.IsNullOrEmpty(SearchNameString))
                {
                    patients = patients.Where(s => s.FirstName.Contains(SearchNameString) || s.LastName.Contains(SearchNameString));


                }
                if (!string.IsNullOrEmpty(SearchPhoneString))
                {
                    patients = patients.Where(s => s.PhoneNumber.Contains(SearchPhoneString));

                }



                Patients = await patients.ToListAsync();
            }

        }
    }
}
