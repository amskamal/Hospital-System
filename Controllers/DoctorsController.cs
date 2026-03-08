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
    public class DoctorsController : Controller
    {
        ApplicationDbContext applicationDbContext = new ApplicationDbContext();

        public IActionResult Index(string searchText, int departmentId, string sortType, int pageSize, int pageNumber)
        {
            ViewBag.AllDepartments = applicationDbContext.Departments;
            ViewBag.currentSearch = searchText;
            ViewBag.selectDepartmentId = departmentId;
            List<Doctor> doctors = new List<Doctor>();

            // paging

            if (pageSize > 0 && pageNumber > 0)
            {
                ViewBag.pageSize = pageSize;
                ViewBag.pageNUmber = pageNumber;
                return View(applicationDbContext.Doctors.Skip(pageSize * (pageNumber - 1)).Take(pageSize));
            }

            // sorting

            if (string.IsNullOrWhiteSpace(sortType) == false && sortType.Trim() == "asc")
            {
                return View(applicationDbContext.Doctors.OrderBy(d => d.DoctorName).ToList());
            }
            else if (string.IsNullOrWhiteSpace(sortType) == false && sortType.Trim() == "desc")
            {
                return View(applicationDbContext.Doctors.OrderByDescending(d => d.DoctorName).ToList());
            }

            // searching

            if (string.IsNullOrWhiteSpace(searchText) == true && departmentId <= 0)  // da eshan lw l user mda5alsh l search id aw 3amal space
            {
                return View(applicationDbContext.Doctors);
            }

            if (departmentId > 0 && string.IsNullOrWhiteSpace(searchText) == true)
            {
                doctors = applicationDbContext.Doctors.Where(d => d.DepartmentId == departmentId).ToList();
            }

            if (departmentId <= 0 && string.IsNullOrWhiteSpace(searchText) == false)
            {
                doctors = applicationDbContext.Doctors.Where(d => d.DoctorName.Contains(searchText.Trim())).ToList();
            }


            if (departmentId > 0 && string.IsNullOrWhiteSpace(searchText) == false)
            {
                doctors = applicationDbContext.Doctors.Where((d => d.DepartmentId == departmentId && d.DoctorName.Contains(searchText.Trim()))).ToList();
            }


            return View(doctors);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.AllDepartments = applicationDbContext.Departments.ToList();
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Doctor doctor)
        {
            if (ModelState.IsValid == true && doctor.DepartmentId > 0)
            {
                doctor.CreatingTime = DateTime.Now;
                applicationDbContext.Doctors.Add(doctor);

                applicationDbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.AllDepartments = applicationDbContext.Departments.ToList();
                return View(doctor);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Doctor doctor = applicationDbContext.Doctors.Include(d => d.Department).FirstOrDefault(d => d.DocId == id);
            if (doctor == null)
            {
                return NotFound();
            }
            else
            {
                ViewBag.AllDepartments = applicationDbContext.Departments.ToList();
                return View(doctor);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Doctor doctor)
        {
            if (id != doctor.DocId)
            {
                return NotFound();
            }
            if (ModelState.IsValid == true)
            {
                doctor.UpdateDataTime = DateTime.Now;
                applicationDbContext.Doctors.Update(doctor);
                applicationDbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AllDepartments = applicationDbContext.Departments.ToList();
            return View(doctor);
        }

        public IActionResult Details(int id)
        {
            Doctor doctor = applicationDbContext.Doctors.Include(d => d.Department).FirstOrDefault(d => d.DocId == id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Doctor doctor = applicationDbContext.Doctors.Find(id);

            if (doctor == null)
            {
                return NotFound();
            }
            else
            {
                return View(doctor);
            }
        }

        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        [HttpPost]
        public IActionResult DeleteConfirmation(int id)
        {
            Doctor doctor = applicationDbContext.Doctors.Find(id);
            if (doctor == null)
            {
                return NotFound();
            }
            else
            {
                applicationDbContext.Doctors.Remove(doctor);
                applicationDbContext.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
