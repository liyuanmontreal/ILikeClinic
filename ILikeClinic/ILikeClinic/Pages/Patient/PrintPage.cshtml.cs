using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ILikeClinic.Pages.Patient
{
    public class PrintPageModel : PageModel
    {
        [BindProperty]
        public ILikeClinic.Model.MedicalHistory MedicalHistory { get; set; }
        public void OnGet()
        {
        }
    }
}
