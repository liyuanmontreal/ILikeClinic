using ILikeClinic.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ILikeClinic.Pages.Admin
{
    public class Patient_ListModel : PageModel
    {
        private readonly ApplicationDbContext _db;

       

        public IList<ILikeClinic.Model.Patient> Patients { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchNameString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchPhoneString { get; set; }

        public Patient_ListModel(ILikeClinic.Data.ApplicationDbContext context)
        {
            _db = context;
        }

        public async Task OnGetAsync()
        {
            if (_db.Patient != null)
            {
                // using system.linq
                var patients = from a in _db.Patient
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
