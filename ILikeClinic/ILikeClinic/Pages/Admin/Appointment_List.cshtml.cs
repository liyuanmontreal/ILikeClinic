using ILikeClinic.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ILikeClinic.Pages.Admin
{
    public class Appointment_ListModel : PageModel
    {
            [BindProperty]
            public IEnumerable<ILikeClinic.Model.Appointment> Appointment { get; set; }

            [BindProperty]
            public ILikeClinic.Model.Patient Patient { get; set; }

            [BindProperty(SupportsGet = true)]
            public string Search { get; set; }

            public readonly ApplicationDbContext _db;

            private readonly IHttpContextAccessor _httpContextAccessor;

            public Appointment_ListModel(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
            {
                _db = db;
                _httpContextAccessor = httpContextAccessor;
                //getPatient();
            }

            private void getPatient()
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var query = _db.Patient.AsNoTracking();

                var result = query.Where(s => s.UserId.Equals(userId));
                if (result.Count() > 0)
                {
                    Patient = result.First();
                }
            }

            public void OnGet()
            {
                var query = _db.Appointment.AsNoTracking();
                if (!string.IsNullOrEmpty(Search))
                {
                    query = query.Where(s => s.Doctor.FirstName.Contains(Search) || s.Doctor.LastName.Contains(Search));
                }
             query = query.Include(c => c.Patient);
             Appointment = query.Include(c => c.Doctor);
              
            }
          
        }
    }