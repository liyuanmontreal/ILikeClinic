using ILikeClinic.Data;
using ILikeClinic.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using System.Data;
using System.Numerics;
using System.Security.Claims;

namespace ILikeClinic.Pages.Doctors
{
    [Authorize(Roles = "Doctor")]
    [BindProperties]
    public class UpsertModel : PageModel
    {
        private static readonly string DEFAULT_PHOTO = "default.jpg";
        private readonly ApplicationDbContext _DB;
        private readonly IWebHostEnvironment _HostEnvironment;
        private readonly IHttpContextAccessor _HttpContextAccessor;

        public Doctor Doctor { get; set; }

        public Gender Gender { get; set; }

        public UpsertModel(ApplicationDbContext db, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _DB = db;
            _HostEnvironment = hostEnvironment;
            _HttpContextAccessor = httpContextAccessor;
            getDoctor();
        }

        public void OnGet()
        {

        }

        private void getDoctor()
        {
            var userId = _HttpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var query = _DB.Doctor.AsNoTracking();

            var result = query.Where(s => s.UserId.Equals(userId));
            if (result.Count() > 0)
            {
                Doctor = result.First();
            }
        }

        public async Task<IActionResult> OnPost()
        {
            Doctor.UserId = _HttpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            string webRootPath = _HostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            updateProfilePhoto(Doctor);
            if (Doctor.Id == 0)
            {// create
                _DB.Doctor.Add(Doctor);
            }
            else
            {//edit
                _DB.Doctor.Update(Doctor);
            }
            _DB.SaveChanges();
            //TempData["success"] = "Your profile has been updated successfully";
            return RedirectToPage("ProfileDetail");
        }

        private void updateProfilePhoto(Doctor doctor)
        {
            var files = HttpContext.Request.Form.Files;
            if (files.Count == 0)
            {
                doctor.ProfilePhoto = @"\images\doctorPhotos\" + DEFAULT_PHOTO;
                return;
            }

            // rename photo
            string webRootPath = _HostEnvironment.WebRootPath;
            string fileUniqueID = Guid.NewGuid().ToString();
            var filePath = Path.Combine(webRootPath, @"images\doctorPhotos");
            var extension = Path.GetExtension(files[0].FileName);
            using (var fileStream = new FileStream(Path.Combine(filePath, fileUniqueID + extension), FileMode.Create))
            {
                files[0].CopyTo(fileStream);
            }

            if (doctor.ProfilePhoto != null)
            {
                var oldImagePath = Path.Combine(webRootPath, doctor.ProfilePhoto.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            doctor.ProfilePhoto = @"\images\doctorPhotos\" + fileUniqueID + extension;
        }
    }
}
