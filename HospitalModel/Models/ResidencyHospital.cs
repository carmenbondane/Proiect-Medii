using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bondane_Carmen_Proiect.Models
{
    public class ResidencyHospital
    {
        public int ResidencyHospitalID { get; set; }
        [Required]
        [Display(Name = "Residency Hospital Name")]
        [StringLength(40)]
        public string HospitalName { get; set; }

        [StringLength(40)]
        public string Adress { get; set; }
 
        public ICollection<TrainedDoctor> TrainedDoctors { get; set; }
    }
}
