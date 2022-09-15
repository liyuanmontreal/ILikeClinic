using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ILikeClinic.Pages
{
    [Authorize(Roles = "Patient")]
    public class PatientModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
