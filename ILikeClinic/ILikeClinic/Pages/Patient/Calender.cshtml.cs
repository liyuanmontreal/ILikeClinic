using ILikeClinic.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

/*namespace ILikeClinic.Pages.Patient
{
    public class CalenderModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string patientId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string doctorId { get; set; }

        public ILikeClinic.Model.Patient Patient { get; set; }

        public readonly ApplicationDbContext _db;

        private readonly IHttpContextAccessor _httpContextAccessor;
        public CalenderModel(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {

            _db = db;
            _httpContextAccessor = httpContextAccessor;
            getPatient();
        }
        public void OnGet()
        {

            //UserId =  _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //get patientId
           
        }


       

        private void getPatient()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var query = _db.Patient.AsNoTracking();
            var result = query.Where(s => s.UserId.Equals(userId));
            if (result.Count() > 0)
            {
                Patient = result.First();
                //doctor = _db.Doctor.Find(Patient.DoctorId);
                patientId = Convert.ToString(Patient.Id);
                doctorId =  Convert.ToString(Patient.DoctorId);

            }
        }
    }

}*/



namespace ILikeClinic.Pages.Patient
{
    public class CalenderModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public CalenderModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
