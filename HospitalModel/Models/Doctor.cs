using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bondane_Carmen_Proiect.Models
{
    public class Doctor
    {
        public int DoctorID { get; set; }

        [Required]
        [Display(Name = "Doctor name")]
        [StringLength(60)]
        public string DoctorName { get; set; }

        [Display(Name = "Doctor specialty")]
        [StringLength(25)]
        public string Specialty { get; set; }

        public ICollection<TrainedDoctor> TrainedDoctors { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
