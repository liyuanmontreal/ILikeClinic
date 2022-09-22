using ILikeClinic.Data;
using ILikeClinic.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ILikeClinic.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _DB;
        public IEnumerable<Doctor> Doctors { get; set; } = default!;
      
        //public IEnumerable<Message> Messages { get; set; } = default!;

        //Constructor
        public IndexModel(ApplicationDbContext db)
        {
            _DB = db;
        }
        public void OnGet()
        {
            Doctors = _DB.Doctor;
          //  Messages = _DB.Message;
        }
    }
}
//    @Html.DisplayFor(modelItem => item.dateofBirth)
//@Html.DisplayFor(modelItem => item.MedicalCardNo)


