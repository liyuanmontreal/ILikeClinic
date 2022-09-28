using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ILikeClinic.Data;
using ILikeClinic.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ILikeClinic.Service;

namespace ILikeClinic.Controller
{
    [Route("api/Appointments")]
    [ApiController]
    public class AppointmentSlotController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public AppointmentSlotController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Appointments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentSlot>>> GetAppointmentSlot([FromQuery] DateTime start, [FromQuery] DateTime end, [FromQuery] int? doctor)
        {
            if (doctor == null)
            {
                return await _context.AppointmentSlot.Where(e => !((e.End <= start) || (e.Start >= end))).Include(e => e.Doctor).ToListAsync();
            }
            else
            {
                return await _context.AppointmentSlot.Where(e => e.Doctor.Id == doctor && !((e.End <= start) || (e.Start >= end))).Include(e => e.Doctor).ToListAsync();
            }

            //return await _context.Appointments.ToListAsync();
        }

        [HttpGet("free")]
        public async Task<ActionResult<IEnumerable<AppointmentSlot>>> GetAppointmentSlot([FromQuery] DateTime start, [FromQuery] DateTime end, [FromQuery] string patient)
        {
            return await _context.AppointmentSlot.Where(e => (e.Status == "free" || (e.Status != "free" && e.PatientId == patient)) && !((e.End <= start) || (e.Start >= end))).Include(e => e.Doctor).ToListAsync();
        }

        // GET: api/Appointments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentSlot>> GetAppointmentSlot(int id)
        {
            var appointmentSlot = await _context.AppointmentSlot.FindAsync(id);

            if (appointmentSlot == null)
            {
                return NotFound();
            }

            return appointmentSlot;
        }

        // PUT: api/Appointments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointmentSlot(int id, AppointmentSlotUpdate update)
        {
            var appointmentSlot = await _context.AppointmentSlot.FindAsync(id);
            if (appointmentSlot == null)
            {
                return NotFound();
            }

            appointmentSlot.Start = update.Start;
            appointmentSlot.End = update.End;

            if (update.Name != null)
            {
                appointmentSlot.PatientName = update.Name;
            }
            if (update.Status != null)
            {
                appointmentSlot.Status = update.Status;
            }

            _context.AppointmentSlot.Update(appointmentSlot);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        public class AppointmentSlotUpdate
        {
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
            public string? Name { get; set; }
            public string? Status { get; set; }

        }


        // PUT: api/Appointments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/request")]
        public async Task<IActionResult> PutAppointmentSlotRequest(int id, AppointmentSlotRequest slotRequest)
        {
            var appointmentSlot = await _context.AppointmentSlot.FindAsync(id);
            if (appointmentSlot == null)
            {
                return NotFound();
            }

            appointmentSlot.PatientName = slotRequest.Name;
            appointmentSlot.PatientId = slotRequest.Patient;
            appointmentSlot.Status = "waiting";

            _context.AppointmentSlot.Update(appointmentSlot);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        public class AppointmentSlotRequest
        {
            public string Patient { get; set; }
            public string Name { get; set; }

        }

        // POST: api/Appointments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AppointmentSlot>> PostAppointmentSlot(AppointmentSlot appointmentSlot)
        {
            _context.AppointmentSlot.Add(appointmentSlot);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppointmentSlot", new { id = appointmentSlot.Id }, appointmentSlot);
        }

        [HttpPost("create")]
        public async Task<ActionResult<AppointmentSlot>> PostAppointmentSlots(AppointmentSlotRange range)
        {

            var doctor = await _context.Doctor.FindAsync(range.Resource);

            if (doctor == null)
            {
                return BadRequest();
            }

            var slots = Timeline.GenerateSlots(range.Start, range.End, range.Scale);
            slots.ForEach(slot => {
                slot.Doctor = doctor;
                _context.AppointmentSlot.Add(slot);
            });

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("clear")]
        public async Task<ActionResult<AppointmentSlot>> PostAppointmentClear(ClearRange range)
        {
            var start = range.Start;
            var end = range.End;

            _context.AppointmentSlot.RemoveRange(_context.AppointmentSlot.Where(e => e.Status == "free" && !((e.End <= start) || (e.Start >= end))));
            await _context.SaveChangesAsync();

            return NoContent();
        }


        public class ClearRange
        {
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
        }

        public class AppointmentSlotRange
        {
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
            public int Resource { get; set; }

            public string Scale { get; set; }
        }

        // DELETE: api/Appointments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointmentSlot(int id)
        {
            var appointmentSlot = await _context.AppointmentSlot.FindAsync(id);
            if (appointmentSlot == null)
            {
                return NotFound();
            }

            _context.AppointmentSlot.Remove(appointmentSlot);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppointmentSlotExists(int id)
        {
            return _context.AppointmentSlot.Any(e => e.Id == id);
        }
    }
}




namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        
    }
}
