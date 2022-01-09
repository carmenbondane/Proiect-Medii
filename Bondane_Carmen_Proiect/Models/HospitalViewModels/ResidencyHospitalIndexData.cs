using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bondane_Carmen_Proiect.Models.HospitalViewModels
{
    public class ResidencyHospitalIndexData
    {
        public IEnumerable<ResidencyHospital> ResidencyHospitals { get; set; }
        public IEnumerable<Doctor> Doctors { get; set; }
        public IEnumerable<Appointment> Appointments { get; set; }
    }
}
