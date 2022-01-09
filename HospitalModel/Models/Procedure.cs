using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bondane_Carmen_Proiect.Models
{
    public class Procedure
    {
        public int ProcedureID { get; set; }

        [Display(Name = "Procedure description")]
        [StringLength(150)]
        public string Description { get; set; }
        public int Price { get; set; }

        public ICollection<Appointment> Appointments { get; set; }

    }
}
