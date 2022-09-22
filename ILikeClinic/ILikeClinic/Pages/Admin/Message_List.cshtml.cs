using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ILikeClinic.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class Message_ListModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
