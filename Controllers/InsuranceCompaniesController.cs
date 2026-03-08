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
    public class InsuranceCompaniesController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();

        // GET: InsuranceCompanies
        public async Task<IActionResult> Index(string searchText, string sortType, int pageSize, int pageNumber)
        {
            ViewBag.AllCompanies = _context.Doctors;
            ViewBag.currentSearch = searchText;
            List<InsuranceCompany> insuranceCompanies = new List<InsuranceCompany>();

            if (pageSize > 0 && pageNumber > 0)
            {
                ViewBag.pageSize = pageSize;
                ViewBag.pageNumber = pageNumber;
                return View(_context.InsuranceCompanies.Skip(pageSize * (pageNumber - 1)).Take(pageSize));
            }

            if (string.IsNullOrWhiteSpace(sortType) == false && sortType.Trim() == "asc")
            {
                return View(_context.InsuranceCompanies.OrderBy(p => p.InsuranceCompanyName).ToList());
            }
            else if (string.IsNullOrWhiteSpace(sortType) == false && sortType.Trim() == "desc")
            {
                return View(_context.InsuranceCompanies.OrderByDescending(p => p.InsuranceCompanyName).ToList());
            }

            if (string.IsNullOrWhiteSpace(searchText) == true)
            {
                return View(_context.InsuranceCompanies);
            }
            else
            {
                insuranceCompanies = _context.InsuranceCompanies.Where(i => i.InsuranceCompanyName.Contains(searchText.Trim())).ToList();
            }

            return View(insuranceCompanies);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(InsuranceCompany insuranceCompany)
        {
            if (ModelState.IsValid == true)
            {
                _context.InsuranceCompanies.Add(insuranceCompany);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(insuranceCompany);
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            InsuranceCompany insuranceCompany = _context.InsuranceCompanies.Find(id);

            if (insuranceCompany == null)
            {
                return NotFound();
            }
            else
            {
                return View(insuranceCompany);
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(int id, InsuranceCompany insuranceCompany)
        {
            if (id != insuranceCompany.InsuranceCompanyId)
            {
                return NotFound();
            }
            if (ModelState.IsValid == true)
            {
                _context.InsuranceCompanies.Update(insuranceCompany);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(insuranceCompany);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            InsuranceCompany insuranceCompany = _context.InsuranceCompanies.Find(id);
            if (insuranceCompany == null)
            {
                return NotFound();
            }
            else
            {
                return View(insuranceCompany);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            InsuranceCompany insuranceCompany = _context.InsuranceCompanies.Find(id);
            if (insuranceCompany == null)
            {
                return NotFound();
            }
            else
            {
                return View(insuranceCompany);
            }
        }

        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        [HttpPost]
        public IActionResult DeleteConfirmation(int id)
        {
            InsuranceCompany insuranceCompany = _context.InsuranceCompanies.Find(id);

            if (insuranceCompany == null)
            {
                return NotFound();
            }
            else
            {
                _context.InsuranceCompanies.Remove(insuranceCompany);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
