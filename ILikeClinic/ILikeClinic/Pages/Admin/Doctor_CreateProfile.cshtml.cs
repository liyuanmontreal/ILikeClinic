using ILikeClinic.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ILikeClinic.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class Doctor_CreateProfileModel : PageModel
    {

        private readonly ApplicationDbContext _db;

        [BindProperty]
        public ILikeClinic.Model.Doctor Doctor { get; set; } = default!;



        public Doctor_CreateProfileModel(ILikeClinic.Data.ApplicationDbContext context)
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

            Doctor.UserId = userId;
            Response.Cookies.Delete("userid");

            _db.Doctor.Add(Doctor);
            _db.SaveChanges();

            TempData["success"] = "Doctor profile created successfully";


            return RedirectToPage("Doctor_List");
        }


    }
}

