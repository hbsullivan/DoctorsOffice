using System.Collections.Generic;
using System;

namespace DoctorsOffice.Models
{
  public class Doctor
  {
    public int DoctorId { get; set; }
    public string Name { get; set; }
    public string Specialty { get; set; }
    public List<DoctorPatient> JoinEntities {get;}
  
  }
}