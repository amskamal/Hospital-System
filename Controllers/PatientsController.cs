using Hospital.Data;
using Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Controllers
{
    public class PatientsController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        public async Task<IActionResult> Index(string searchText, string sortType, int pageSize, int pageNumber)
        {
            ViewBag.AllDoctors = _context.Doctors;
            ViewBag.currentSearch = searchText;
            List<Patient> patients = new List<Patient>();

            // paging
            if (pageSize > 0 && pageNumber > 0)
            {
                ViewBag.pageSize = pageSize;
                ViewBag.pageNumber = pageNumber;
                return View(_context.patients.Skip(pageSize * (pageNumber - 1)).Take(pageSize));
            }

            //sorting

            if (string.IsNullOrWhiteSpace(sortType) == false && sortType.Trim() == "asc")
            {
                return View(_context.patients.OrderBy(p => p.PatientName).ToList());
            }
            else if (string.IsNullOrWhiteSpace(sortType) == false && sortType.Trim() == "desc")
            {
                return View(_context.patients.OrderByDescending(p => p.PatientName).ToList());
            }

            //searching 
            if (string.IsNullOrWhiteSpace(searchText) == true)
            {
                return View(_context.patients);
            }
            else
            {
                patients = _context.patients.Where(p => p.PatientName.Contains(searchText.Trim())).ToList();
            }
            return View(patients);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Patient patient)
        {
            if (ModelState.IsValid == true)
            {
                patient.CreatingTime = DateTime.Now;
                _context.patients.Add(patient);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(patient);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Patient patient = _context.patients.Find(id);

            if (patient == null)
            {
                return NotFound();
            }
            else
            {
                return View(patient);
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(int id, Patient patient)
        {
            if (id != patient.PatientId)
            {
                return NotFound();
            }
            if (ModelState.IsValid == true)
            {
                patient.UpdateDataTime = DateTime.Now;
                _context.patients.Update(patient);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Patient patient = _context.patients.Find(id);
            if (patient == null)
            {
                return NotFound();
            }
            else
            {
                return View(patient);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Patient patient = _context.patients.Find(id);
            if (patient == null)
            {
                return NotFound();
            }
            else
            {
                return View(patient);
            }
        }

        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        [HttpPost]
        public IActionResult DeleteConfirmation(int id)
        {
            Patient patient = _context.patients.Find(id);

            if (patient == null)
            {
                return NotFound();
            }
            else
            {
                _context.patients.Remove(patient);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
