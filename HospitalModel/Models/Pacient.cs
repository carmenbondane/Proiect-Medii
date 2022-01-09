using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Bondane_Carmen_Proiect.Models
{
    public class Pacient
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PacientID { get; set; }

        [Required]
        [Display(Name = "Pacient name")]
        [StringLength(60)]
        public string PacientName { get; set; }

        [Display(Name = "Pacient address")]
        [StringLength(50)]
        public string Adress { get; set; }
        public DateTime BirthDate { get; set; }
        public int Phone { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
