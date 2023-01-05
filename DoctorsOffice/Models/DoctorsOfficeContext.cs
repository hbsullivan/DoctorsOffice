using Microsoft.EntityFrameworkCore;

namespace DoctorsOffice.Models
{
  public class DoctorsOfficeContext : DbContext
  {
    public DbSet<Doctor> Doctors { get; set; }

    public DbSet<Patient> Patients {get; set;}

    public DbSet<DoctorPatient> DoctorPatients {get; set;}

    public DoctorsOfficeContext(DbContextOptions options) : base(options) { }
  }
}