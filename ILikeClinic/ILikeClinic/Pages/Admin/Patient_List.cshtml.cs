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

        public Patient_ListModel(ILikeClinic.Data.ApplicationDbContext context)
        {
            _db = context;
        }

        public async Task OnGetAsync()
        {
            if (_db.Patient != null)
            {
                Patients = await _db.Patient.ToListAsync();
            }


            


        }
    }
}
