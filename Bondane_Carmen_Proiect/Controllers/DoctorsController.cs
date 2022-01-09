using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bondane_Carmen_Proiect.Data;
using Bondane_Carmen_Proiect.Models;
using Microsoft.AspNetCore.Authorization;

namespace Bondane_Carmen_Proiect.Controllers
{
    [Authorize(Roles = "HeadDoctor")]
    public class DoctorsController : Controller
    {
        private readonly HospitalContext _context;

        public DoctorsController(HospitalContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        // GET: Doctors
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["DoctorNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "doctorname_desc" : "";
            ViewData["SpecialtySortParm"] = String.IsNullOrEmpty(sortOrder) ? "specialty_desc" : "";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            var doctors = from d in _context.Doctors
                        select d;
            if (!String.IsNullOrEmpty(searchString))
            {
                doctors = doctors.Where(s => s.DoctorName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "doctorname_desc":
                    doctors = doctors.OrderByDescending(d => d.DoctorName);
                    break;
                case "specialty_desc":
                    doctors = doctors.OrderByDescending(d => d.Specialty);
                    break;
                default:
                    doctors = doctors.OrderBy(d => d.DoctorName);
                    break;
            }
            int pageSize = 2;
            return View(await PaginatedList<Doctor>.CreateAsync(doctors.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        [AllowAnonymous]
        // GET: Doctors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors
                .Include(s => s.Appointments)
                .ThenInclude(e => e.Pacient)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.DoctorID == id);


            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // GET: Doctors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DoctorID,DoctorName,Specialty")] Doctor doctor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(doctor);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex*/)
            {

                ModelState.AddModelError("", "Unable to save changes. " +
                "Try again, and if the problem persists ");
            }
            return View(doctor);
        }

        // GET: Doctors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var studentToUpdate = await _context.Doctors.FirstOrDefaultAsync(s => s.DoctorID == id);
            if (await TryUpdateModelAsync<Doctor>(
            studentToUpdate,
            "",
            s => s.DoctorName, s => s.Specialty))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists");
                }
            }
            return View(studentToUpdate);
        }

        // GET: Doctors/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors
                 .AsNoTracking()
                .FirstOrDefaultAsync(m => m.DoctorID == id);
            if (doctor == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                "Delete failed. Try again";
            }
            return View(doctor);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Doctors.Remove(doctor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {

                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool DoctorExists(int id)
        {
            return _context.Doctors.Any(e => e.DoctorID == id);
        }
    }
}
