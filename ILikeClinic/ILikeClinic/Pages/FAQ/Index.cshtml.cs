using ILikeClinic.Data;
using ILikeClinic.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
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
            //getFAQ();
        }

        private void getFAQ()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var query = _db.FAQ.AsNoTracking();
            var result = query.Where(s => s.Id!=-1);
            if (result.Count() > 0)
            {
                fAQs = result;
            }
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
