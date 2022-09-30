using ILikeClinic.Data;
using ILikeClinic.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ILikeClinic.Pages.Patient
{
    [Authorize(Roles = "Patient")] 
    public class Message_ListModel : PageModel
    {
        private readonly ApplicationDbContext _DB;
        private readonly IHttpContextAccessor _HttpContextAccessor;

        public IList<Message> Messages { get; set; } = default!;

        [BindProperty]
        public ILikeClinic.Model.Patient Patient { get; set; }
        [BindProperty]
        public ILikeClinic.Model.Doctor Doctor { get; set; }

        public Message_ListModel(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _DB = db;
            _HttpContextAccessor = httpContextAccessor;
            getPatient();
        }

        private void getPatient()
        {
            var userId = _HttpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var query = _DB.Patient.AsNoTracking();

            var result = query.Where(s => s.UserId.Equals(userId));
            if (result.Count() > 0)
            {
                Patient = result.First();
            }
        }

        public async Task OnGetAsync()
        {
           // var userId = _HttpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (_DB.Message != null)
            {
                // using system.linq
                var messages = from a in _DB.Message
                               select a;

                //lambda 
                messages = messages.Where(s => s.FromId == Patient.Id);// || s.ToId == Doctor.Id);

                Messages = await messages.ToListAsync();

            }
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var Message = await _DB.Message.FindAsync(id);
            if (Message == null)
            {
                return NotFound();
            }
            _DB.Message.Remove(Message);
            _DB.SaveChanges();

            TempData["delete"] = "Message deleted successfully!";
            return RedirectToPage();
        }
    }
}
