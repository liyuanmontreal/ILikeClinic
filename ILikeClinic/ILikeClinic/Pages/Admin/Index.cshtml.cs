using ILikeClinic.Data;
using ILikeClinic.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ILikeClinic.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IList<ILikeClinic.Model.Patient> Patients { get; set; } = default!;
        public IList<ILikeClinic.Model.Doctor> Doctors { get; set; } = default!;
        public IList<ILikeClinic.Model.Appointment> Appointments { get; set; } = default!;

        public int doctorNum;
        public int patientNum;
        public int appointmentNum;
        //public IList<ILikeClinic.Model.Message> Messages { get; set; } = default!;

        //Constructor
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
            Doctors= _db.Doctor.ToList();
            Patients = _db.Patient.ToList(); 
            Appointments = _db.Appointment.ToList();
            doctorNum = Doctors.Count;
            patientNum = Patients.Count;
            appointmentNum = Appointments.Count;
            //Messages = _DB.Message;
        }
    }
}
//    @Html.DisplayFor(modelItem => item.dateofBirth)
//@Html.DisplayFor(modelItem => item.MedicalCardNo)


