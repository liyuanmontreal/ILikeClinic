using ILikeClinic.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ILikeClinic.Pages.Admin
    {
        public class Appointment_ListModel : PageModel
        {
         

            [BindProperty]
            public IEnumerable<ILikeClinic.Model.Appointment> Appointments { get; set; }

            [BindProperty]
            public ILikeClinic.Model.Patient Patient { get; set; }

            [BindProperty(SupportsGet = true)]
            public string Search { get; set; }

            public readonly ApplicationDbContext _db;

            private readonly IHttpContextAccessor _httpContextAccessor;


            public Appointment_ListModel(ILikeClinic.Data.ApplicationDbContext context)
            {
                _db = context;
            }

            public async Task OnGetAsync()
            {
                if (_db.Appointment != null)
                {
                Appointments = await _db.Appointment.ToListAsync();
                }


            }
        }
    }

