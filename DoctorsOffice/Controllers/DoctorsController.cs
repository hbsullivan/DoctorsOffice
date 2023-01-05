using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using DoctorsOffice.Models;
using System.Linq;
using System.Collections.Generic;

namespace DoctorsOffice.Controllers
{
  public class DoctorsController : Controller
  {
    private readonly DoctorsOfficeContext _db;
    public DoctorsController(DoctorsOfficeContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Doctors.ToList());
    }

    public ActionResult Create()
    {
      return View();
    }
    [HttpPost]
    public ActionResult Create(Doctor doctor)
    {
      _db.Doctors.Add(doctor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Doctor thisDoctor = _db.Doctors
                            .Include(doctor => doctor.JoinEntities)
                            .ThenInclude(join => join.Patient)
                            .FirstOrDefault(doctor => doctor.DoctorId == id);
      return View(thisDoctor);
    }

     public ActionResult AddPatient(int id)
    {
      Doctor thisDoctor = _db.Doctors.FirstOrDefault(doctors => doctors.DoctorId == id);
      ViewBag.PatientId = new SelectList(_db.Patients, "PatientId", "Name");
      return View(thisDoctor);
    }

    [HttpPost]
    public ActionResult AddPatient(Doctor doctor, int patientId)
    {
      #nullable enable
      DoctorPatient? joinEntity = _db.DoctorPatients.FirstOrDefault(joinEntity => (joinEntity.PatientId == patientId && joinEntity.DoctorId == doctor.DoctorId));
      #nullable disable
      if (joinEntity == null && patientId != 0)
      {
        _db.DoctorPatients.Add(new DoctorPatient() { PatientId = patientId, DoctorId = doctor.DoctorId});
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = doctor.DoctorId });
    }
  }
}