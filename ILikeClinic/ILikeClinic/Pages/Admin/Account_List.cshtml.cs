using ILikeClinic.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ILikeClinic.Pages.Admin
{
    public class Account_ListModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly ApplicationDbContext _db;
        public List<IdentityUser> Users { get; private set; }

      
        public Account_ListModel(
            UserManager<IdentityUser> userManager,            
            ApplicationDbContext db)
        {
            _userManager = userManager;                
            _db = db;       
          
        }
        public void OnGet()
        {
            Users = _userManager.Users.ToList();
        }
    }
}
