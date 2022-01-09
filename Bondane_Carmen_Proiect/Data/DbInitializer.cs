using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bondane_Carmen_Proiect.Models;

namespace Bondane_Carmen_Proiect.Data
{
    public class DbInitializer
    {
        public static void Initialize(HospitalContext context)
        {
            context.Database.EnsureCreated();
            if (context.Pacients.Any())
            {
                return; // BD a fost creata anterior
            }
            //populam baza de date cu Pacienti
            var pacients = new Pacient[]
            {
                new Pacient{PacientID=20,PacientName="Anton Serenela",Adress="Strada Lalelelor, nr. 4, Cluj-Napoca",BirthDate=DateTime.Parse("1992-09-01"), Phone=0729675896},
                new Pacient{PacientID=21,PacientName="Marcu Paul",Adress="Aleea Poporului, bloc C3, ap. 2, Turda",BirthDate=DateTime.Parse("1994-12-04"), Phone=0722365896},
                new Pacient{PacientID=22,PacientName="Popa Marcel",Adress="Strada Mircea Eliade, nr. 23, Deva",BirthDate=DateTime.Parse("1976-11-05"), Phone=0733848294},
                new Pacient{PacientID=23,PacientName="Andrei Adina",Adress="Aleea Progresului, bloc S, ap. 34, Dej",BirthDate=DateTime.Parse("1997-07-11"), Phone=0792001923},
                new Pacient{PacientID=24,PacientName="Pascu Teodor",Adress="Strada Anton Pann, nr. 10, Cluj-Napoca",BirthDate=DateTime.Parse("1972-01-12"), Phone=0745882918}
            };

            foreach (Pacient p in pacients)
            {
                context.Pacients.Add(p);
            }
            context.SaveChanges();

            //populam baza de date cu Doctori
            var doctors = new Doctor[]
            {
                new Doctor{DoctorID=7,DoctorName="Dumitrean Dorin",Specialty="Boli infectioase"},
                new Doctor{DoctorID=8,DoctorName="Groza Angela",Specialty="Cardiologie"},
                new Doctor{DoctorID=9,DoctorName="Burdan Rodica",Specialty="Endocrinologie"},
                new Doctor{DoctorID=10,DoctorName="Pavel Adrian",Specialty="Hematologie"},
                new Doctor{DoctorID=11,DoctorName="Raica Octavia",Specialty="Medicina de familie"},
                new Doctor{DoctorID=12,DoctorName="Pasc Eleonora",Specialty="Pneumologie"},
                new Doctor{DoctorID=13,DoctorName="Popescu Alexandru",Specialty="Psihiatrie"},
                new Doctor{DoctorID=17,DoctorName="Androne Vladimir",Specialty="Radiologie si imagistica"},

            };
            foreach (Doctor d in doctors)
            {
                context.Doctors.Add(d);
            }
            context.SaveChanges();

            //populam baza de date cu Proceduri
            var procedures = new Procedure[]
            {
                new Procedure{ProcedureID=1,Description="Serv profilactice febra galbena",Price=290},
                new Procedure{ProcedureID=2,Description="EKG cu interpretare",Price=220},
                new Procedure{ProcedureID=3,Description="Ecografie tiroidiana doppler",Price=120},
                new Procedure{ProcedureID=4,Description="Hemoleucograma completa",Price=140},
                new Procedure{ProcedureID=5,Description="Consultatie la domiciliu",Price=150},
                new Procedure{ProcedureID=6,Description="Spirometrie cu interpretare",Price=100},
                new Procedure{ProcedureID=9,Description="Sedinta psihoterapie",Price=120},
                new Procedure{ProcedureID=10,Description="Ecografie mamara unilaterala",Price=160},

            };
            foreach (Procedure p in procedures)
            {
                context.Procedures.Add(p);
            }
            context.SaveChanges();

            //populam baza de date cu Programari
            var appointments = new Appointment[]
            {
                new Appointment{DoctorID=3,PacientID=20,ProcedureID=9,AppointmentDate=DateTime.Parse("2021-01-01")},
                new Appointment{DoctorID=10,PacientID=23,ProcedureID=4,AppointmentDate=DateTime.Parse("2021-01-01")},
                new Appointment{DoctorID=8,PacientID=22,ProcedureID=2,AppointmentDate=DateTime.Parse("2021-12-01")},
                new Appointment{DoctorID=11,PacientID=21,ProcedureID=5,AppointmentDate=DateTime.Parse("2021-12-01")},
                new Appointment{DoctorID=9,PacientID=24,ProcedureID=10,AppointmentDate=DateTime.Parse("2021-10-07")},
            };
            foreach (Appointment a in appointments)
            {
                context.Appointments.Add(a);
            }
            context.SaveChanges();

            var residencyhospitals = new ResidencyHospital[]
            {
                new ResidencyHospital{HospitalName="Spitalul Universitar Carol Davila", Adress="Str. Ciupercilor, nr. 41, Bucuresti"},
                new ResidencyHospital{HospitalName="Spitalul Clinic Judetean Cluj-Napoca", Adress="Str. Lalelelor, nr. 23, Cluj-Napoca"},
                new ResidencyHospital{HospitalName="Spitalul Clinic Municipal Timisoara", Adress="Str. Ferigilor, nr. 12, Timisoara"},
            };
            foreach (ResidencyHospital rh in residencyhospitals)
            {
                context.ResidencyHospitals.Add(rh);
            }
            context.SaveChanges();


            var traineddoctors = new TrainedDoctor[]
            {
                 new TrainedDoctor {
                 DoctorID = doctors.Single(c => c.DoctorName == "Dumitrean Dorin" ).DoctorID,
                 ResidencyHospitalID = residencyhospitals.Single(i => i.HospitalName =="Spitalul Universitar Carol Davila").ResidencyHospitalID
                 },
                 new TrainedDoctor {
                 DoctorID = doctors.Single(c => c.DoctorName == "Burdan Rodica" ).DoctorID,
                 ResidencyHospitalID = residencyhospitals.Single(i => i.HospitalName =="Spitalul Clinic Judetean de Urgenta Cluj-Napoca").ResidencyHospitalID
                 },
                 new TrainedDoctor {
                 DoctorID = doctors.Single(c => c.DoctorName == "Pavel Adrian" ).DoctorID,
                 ResidencyHospitalID = residencyhospitals.Single(i => i.HospitalName =="Spitalul Universitar Carol Davila").ResidencyHospitalID
                 },
                 new TrainedDoctor {
                 DoctorID = doctors.Single(c => c.DoctorName == "Raica Octavia" ).DoctorID,
                 ResidencyHospitalID = residencyhospitals.Single(i => i.HospitalName == "Spitalul Clinic Judetean de Urgenta Cluj-Napoca").ResidencyHospitalID
                 },
                 new TrainedDoctor {
                 DoctorID = doctors.Single(c => c.DoctorName == "Popescu Alexandru" ).DoctorID,
                 ResidencyHospitalID = residencyhospitals.Single(i => i.HospitalName == "Spitalul Clinic Judetean de Urgenta Cluj-Napoca").ResidencyHospitalID
                 },
                 new TrainedDoctor {
                 DoctorID = doctors.Single(c => c.DoctorName == "Androne Vladimir" ).DoctorID,
                 ResidencyHospitalID = residencyhospitals.Single(i => i.HospitalName == "Spitalul Clinic Municipal Timisoara").ResidencyHospitalID
                 },
            };
            foreach (TrainedDoctor td in traineddoctors)
            {
                context.TrainedDoctors.Add(td);
            }
            context.SaveChanges();
        }
    }
}
