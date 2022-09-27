using ILikeClinic.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace ILikeClinic.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        ADMIN_ID = 1;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
    public DbSet<Patient> Patient { get; set; }
    public DbSet<Appointment> Appointment { get; set; }
    public DbSet<FAQ> FAQ  { get; set; }
    public DbSet<Doctor> Doctor { get; set; }
    public DbSet<Availability> Availability { get; set; }
    public DbSet<MedicalHistory> MedicalHistory { get; set; }
    public DbSet<Message> Message { get; set; }
    public DbSet<Survey> Survey { get; set; }

    public int ADMIN_ID;
}