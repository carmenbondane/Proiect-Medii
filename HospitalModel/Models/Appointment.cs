using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bondane_Carmen_Proiect.Models
{
    public class Appointment
    {
        public int AppointmentID { get; set; }
        public int DoctorID { get; set; }
        public int PacientID { get; set; }
        public int ProcedureID { get; set; }
        public DateTime AppointmentDate { get; set; }

        public Doctor Doctor { get; set; }
        public Pacient Pacient { get; set; }
        public Procedure Procedure { get; set; }
    }
}
