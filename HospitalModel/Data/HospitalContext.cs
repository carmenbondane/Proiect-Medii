using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bondane_Carmen_Proiect.Models;

namespace Bondane_Carmen_Proiect.Data
{
    public class HospitalContext : DbContext
    {
        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options)
        {
        }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Pacient> Pacients { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<ResidencyHospital> ResidencyHospitals { get; set; }
        public DbSet<TrainedDoctor> TrainedDoctors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>().ToTable("Doctor");
            modelBuilder.Entity<Pacient>().ToTable("Pacient");
            modelBuilder.Entity<Procedure>().ToTable("Procedure");
            modelBuilder.Entity<Appointment>().ToTable("Appointment");
            modelBuilder.Entity<ResidencyHospital>().ToTable("ResidencyHospital");
            modelBuilder.Entity<TrainedDoctor>().ToTable("TrainedDoctor");
            modelBuilder.Entity<TrainedDoctor>()
            .HasKey(c => new { c.DoctorID, c.ResidencyHospitalID });//configureaza cheia primara compusa
        }
    }
}
