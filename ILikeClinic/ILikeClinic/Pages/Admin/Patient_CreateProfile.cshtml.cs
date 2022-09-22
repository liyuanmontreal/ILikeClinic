using ILikeClinic.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ILikeClinic.Pages.Admin
{
    public class Patient_CreateProfileModel : PageModel
    {

        private readonly ApplicationDbContext _db;

        [BindProperty]
        public ILikeClinic.Model.Patient Patient { get; set; } = default!;



        public Patient_CreateProfileModel(ILikeClinic.Data.ApplicationDbContext context)
        {
            _db = context;
        }
        public async Task<IActionResult> OnGetAsync()
        {
          
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            //read cookie from Request object  
            //string userId = Request.Cookies["userid"];
            var userId = HttpContext.Request.Cookies["userid"];

            Patient.UserId = userId;
            Response.Cookies.Delete("userid");

            _db.Patient.Add(Patient);
            _db.SaveChanges();

            TempData["success"] = "Patient profile created successfully";
         

            return RedirectToPage("Patient_List");
        }

     
    }
}
