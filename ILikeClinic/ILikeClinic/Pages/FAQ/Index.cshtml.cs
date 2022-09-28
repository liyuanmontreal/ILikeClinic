//using Microsoft.Graph;
using ILikeClinic.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ILikeClinic.Pages.FAQ
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public IEnumerable<ILikeClinic.Model.FAQ> fAQs { get; set; }

        [BindProperty]
        public IdentityUser IdentityUser { get; set; }

        public readonly ApplicationDbContext _db;

        private readonly IHttpContextAccessor _httpContextAccessor;


        public IndexModel(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            
        }

        public void OnGet()
        {
            fAQs = _db.FAQ;
            /* if (Patient == null)
                 return RedirectToPage("AddMyFile");
             return Page();
            */
        }
    }
}
