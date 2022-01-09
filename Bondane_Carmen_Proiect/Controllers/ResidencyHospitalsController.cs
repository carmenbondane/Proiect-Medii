using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bondane_Carmen_Proiect.Data;
using Bondane_Carmen_Proiect.Models;
using Bondane_Carmen_Proiect.Models.HospitalViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Bondane_Carmen_Proiect.Controllers
{
    [Authorize(Policy = "OnlyHeadDoctor")]
    public class ResidencyHospitalsController : Controller
    {
        private readonly HospitalContext _context;

        public ResidencyHospitalsController(HospitalContext context)
        {
            _context = context;
        }

        // GET: ResidencyHospitals
        public async Task<IActionResult> Index(int? id, int? doctorID)
        {
            var viewModel = new ResidencyHospitalIndexData();
            viewModel.ResidencyHospitals = await _context.ResidencyHospitals
            .Include(i => i.TrainedDoctors)
            .ThenInclude(i => i.Doctor)
            .ThenInclude(i => i.Appointments)
            .ThenInclude(i => i.Pacient)
            .AsNoTracking()
            .OrderBy(i => i.HospitalName)
            .ToListAsync();
            if (id != null)
            {
                ViewData["ResidencyHospitalID"] = id.Value;
                ResidencyHospital residencyhospital = viewModel.ResidencyHospitals.Where(
                i => i.ResidencyHospitalID == id.Value).Single();
                viewModel.Doctors = residencyhospital.TrainedDoctors.Select(s => s.Doctor);
            }
            if (doctorID != null)
            {
                ViewData["DoctorID"] = doctorID.Value;
                viewModel.Appointments = viewModel.Doctors.Where(
                x => x.DoctorID == doctorID).Single().Appointments;
            }
            return View(viewModel);
        }

        // GET: ResidencyHospitals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var residencyHospital = await _context.ResidencyHospitals
                .FirstOrDefaultAsync(m => m.ResidencyHospitalID == id);
            if (residencyHospital == null)
            {
                return NotFound();
            }

            return View(residencyHospital);
        }

        // GET: ResidencyHospitals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ResidencyHospitals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResidencyHospitalID,HospitalName,Adress")] ResidencyHospital residencyHospital)
        {
            if (ModelState.IsValid)
            {
                _context.Add(residencyHospital);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(residencyHospital);
        }

        // GET: ResidencyHospitals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var residencyhospital = await _context.ResidencyHospitals
                .Include(i => i.TrainedDoctors).ThenInclude(i => i.Doctor)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ResidencyHospitalID == id);

            if (residencyhospital == null)
            {
                return NotFound();
            }
            PopulateTrainedDoctorData(residencyhospital);
            return View(residencyhospital);
        }
        private void PopulateTrainedDoctorData(ResidencyHospital residencyhospital)
        {
            var allDoctors = _context.Doctors;
            var residencyhospitaldoctors = new HashSet<int>(residencyhospital.TrainedDoctors.Select(c => c.DoctorID));
            var viewModel = new List<TrainedDoctorData>();
            foreach (var doctor in allDoctors)
            {
                viewModel.Add(new TrainedDoctorData
                {
                    DoctorID = doctor.DoctorID,
                    DoctorName = doctor.DoctorName,
                    IsTrained = residencyhospitaldoctors.Contains(doctor.DoctorID)
                });
            }
            ViewData["Doctors"] = viewModel;
        }

        // POST: ResidencyHospitals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selecteddoctors)
        {
            if (id == null)
            {
                return NotFound();
            }

            var residencyhospitalToUpdate = await _context.ResidencyHospitals
             .Include(i => i.TrainedDoctors)
             .ThenInclude(i => i.Doctor)
             .FirstOrDefaultAsync(m => m.ResidencyHospitalID == id);

            if (await TryUpdateModelAsync<ResidencyHospital>(
            residencyhospitalToUpdate,
            "",
            i => i.HospitalName, i => i.Adress))
            {
                UpdateTrainedDoctors(selecteddoctors, residencyhospitalToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {

                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, ");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdateTrainedDoctors(selecteddoctors, residencyhospitalToUpdate);
            PopulateTrainedDoctorData(residencyhospitalToUpdate);
            return View(residencyhospitalToUpdate);
        }

        private void UpdateTrainedDoctors(string[] selecteddoctors, ResidencyHospital residencyhospitalToUpdate)
        {
            if (selecteddoctors == null)
            {
                residencyhospitalToUpdate.TrainedDoctors = new List<TrainedDoctor>();
                return;
            }
            var selectedDoctorsHS = new HashSet<string>(selecteddoctors);
            var trainedDoctors = new HashSet<int>
            (residencyhospitalToUpdate.TrainedDoctors.Select(c => c.Doctor.DoctorID));
            foreach (var doctor in _context.Doctors)
            {
                if (selectedDoctorsHS.Contains(doctor.DoctorID.ToString()))
                {
                    if (!trainedDoctors.Contains(doctor.DoctorID))
                    {
                        residencyhospitalToUpdate.TrainedDoctors.Add(new TrainedDoctor
                        {
                            ResidencyHospitalID =
                       residencyhospitalToUpdate.ResidencyHospitalID,
                            DoctorID = doctor.DoctorID
                        });
                    }
                }
                else
                {
                    if (trainedDoctors.Contains(doctor.DoctorID))
                    {
                        TrainedDoctor doctorToRemove = residencyhospitalToUpdate.TrainedDoctors.FirstOrDefault(i
                       => i.DoctorID == doctor.DoctorID);
                        _context.Remove(doctorToRemove);
                    }
                }
            }
        }

        // GET: ResidencyHospitals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var residencyHospital = await _context.ResidencyHospitals
                .FirstOrDefaultAsync(m => m.ResidencyHospitalID == id);
            if (residencyHospital == null)
            {
                return NotFound();
            }

            return View(residencyHospital);
        }

        // POST: ResidencyHospitals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var residencyHospital = await _context.ResidencyHospitals.FindAsync(id);
            _context.ResidencyHospitals.Remove(residencyHospital);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResidencyHospitalExists(int id)
        {
            return _context.ResidencyHospitals.Any(e => e.ResidencyHospitalID == id);
        }
    }
}
