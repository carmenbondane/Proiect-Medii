using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bondane_Carmen_Proiect.Data;
using Bondane_Carmen_Proiect.Models;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace Bondane_Carmen_Proiect.Controllers
{
    [Authorize(Policy = "HospitalManager")]
    public class PacientsController : Controller
    {
        private readonly HospitalContext _context;
        private string _baseUrl = "http://localhost:59227/api/Pacients";

        public PacientsController(HospitalContext context)
        {
            _context = context;
        }

        // GET: Pacients
        public async Task<IActionResult> Index()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(_baseUrl);
            if (response.IsSuccessStatusCode)
            {
                var pacients = JsonConvert.DeserializeObject<List<Pacient>>(await
               response.Content.
                ReadAsStringAsync());
                return View(pacients);
            }
            return NotFound();
        }

        // GET: Pacients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }

            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}/{id.Value}");
            if (response.IsSuccessStatusCode)
            {
                var pacient = JsonConvert.DeserializeObject<Pacient>(
                await response.Content.ReadAsStringAsync());
                return View(pacient);
            }
            return NotFound();
        }

        // GET: Pacients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pacients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PacientID,PacientName,Adress,BirthDate,Phone")] Pacient pacient)
        {
            if (ModelState.IsValid) return View(pacient);
            try
            {
                var client = new HttpClient();
                string json = JsonConvert.SerializeObject(pacient);
                var response = await client.PostAsync(_baseUrl,
                new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to create record:{ex.Message}");
            }
            return View(pacient);
        }

        // GET: Pacients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }

            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}/{id.Value}");
            if (response.IsSuccessStatusCode)
            {
                var pacient = JsonConvert.DeserializeObject<Pacient>(
                await response.Content.ReadAsStringAsync());
                return View(pacient);
            }
            return new NotFoundResult();
        }

        // POST: Pacients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PacientID,PacientName,Adress,BirthDate,Phone")] Pacient pacient)
        {
            if (!ModelState.IsValid) return View(pacient);
            var client = new HttpClient();
            string json = JsonConvert.SerializeObject(pacient);
            var response = await client.PutAsync($"{_baseUrl}/{pacient.PacientID}",
            new StringContent(json, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(pacient);
        }

        // GET: Pacients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }

            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}/{id.Value}");
            if (response.IsSuccessStatusCode)
            {
                var pacient = JsonConvert.DeserializeObject<Pacient>(await
               response.Content.ReadAsStringAsync());
                return View(pacient);
            }
            return new NotFoundResult();
        }

        // POST: Pacients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([Bind("PacientID")] Pacient pacient)
        {
            try
            {
                var client = new HttpClient();
                HttpRequestMessage request =
                new HttpRequestMessage(HttpMethod.Delete,
               $"{_baseUrl}/{pacient.PacientID}")
                {
                    Content = new StringContent(JsonConvert.SerializeObject(pacient),
               Encoding.UTF8, "application/json")
                };
                var response = await client.SendAsync(request);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to delete record:{ex.Message}");
            }
            return View(pacient);
        }

        private bool PacientExists(int id)
        {
            return _context.Pacients.Any(e => e.PacientID == id);
        }
    }
}
