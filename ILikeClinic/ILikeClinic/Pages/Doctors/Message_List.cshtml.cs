using ILikeClinic.Data;
using ILikeClinic.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ILikeClinic.Pages.Doctors
{
    [Authorize(Roles = "Doctor")] 
    public class Message_ListModel : PageModel
    {
        private readonly ApplicationDbContext _DB;
        private readonly IHttpContextAccessor _HttpContextAccessor;

        public IList<Message> Messages { get; set; } = default!;

        [BindProperty]
        public Doctor Doctor { get; set; }

        public Message_ListModel(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _DB = db;
            _HttpContextAccessor = httpContextAccessor;
            getDoctor();
        }

        private void getDoctor()
        {
            var userId = _HttpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var query = _DB.Doctor.AsNoTracking();

            var result = query.Where(s => s.UserId.Equals(userId));
            if (result.Count() > 0)
            {
                Doctor = result.First();
            }
        }

        public async Task OnGetAsync()
        {
            var userId = _HttpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (_DB.Message != null)
            {
                // using system.linq
                var messages = from a in _DB.Message
                               select a;

                //lambda 
                messages = messages.Where(s => s.ToId == Doctor.Id);// || s.ToId == Doctor.Id);

                Messages = await messages.ToListAsync();
            }
        }
    }
}
