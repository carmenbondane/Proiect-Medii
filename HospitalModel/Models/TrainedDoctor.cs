using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bondane_Carmen_Proiect.Models
{
    public class TrainedDoctor
    {
        public int ResidencyHospitalID { get; set; }
        public int DoctorID { get; set; }

        public ResidencyHospital ResidencyHospital { get; set; }
        public Doctor Doctor { get; set; }
    }
}
