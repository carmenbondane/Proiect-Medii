using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Bondane_Carmen_Proiect.Models.HospitalViewModels
{
    public class AppointmentGroup
    {
        [DataType(DataType.Date)]
        public DateTime? AppointmentDate { get; set; }
        public int PacientCount { get; set; }
    }
}
