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
    public class VisitsController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();

        public async Task<IActionResult> Index(string doctorSearchText, string patientSearchtext,string insuranceSearchText, int doctorId, int patientId, int insuranceId, int pageSize, int pageNumber)
        {
            ViewBag.AllDoctors = _context.Doctors;
            ViewBag.AllPatients = _context.patients;
            ViewBag.AllInsuranceCompanies = _context.InsuranceCompanies;
            ViewBag.doctorSearchText = doctorSearchText;
            ViewBag.patientSearchtext = patientSearchtext;
            ViewBag.insuranceSearchText = insuranceSearchText;
            ViewBag.selectDoctorId = doctorId;
            ViewBag.selectPatientId = patientId;
            ViewBag.selectInsuranceId = insuranceId;
            List<Visit> visits = new List<Visit>();
            
            // paging
            if (pageSize > 0 && pageNumber > 0)
            {
                ViewBag.pageSize = pageSize;
                ViewBag.pageNumber = pageNumber;
                return View(_context.Visits.Skip(pageSize * (pageNumber - 1)).Take(pageSize));
            }

            //search 
            if (string.IsNullOrWhiteSpace(doctorSearchText) == true && doctorId <= 0 && string.IsNullOrWhiteSpace(patientSearchtext) == true && patientId <= 0 && string.IsNullOrWhiteSpace(insuranceSearchText) == true && insuranceId<=0)
            {
                return View(_context.Visits);
            }

            //searching by doctor ID and name

            if (string.IsNullOrWhiteSpace(doctorSearchText) == true && doctorId > 0 && string.IsNullOrWhiteSpace(patientSearchtext) == true && patientId <= 0 && string.IsNullOrWhiteSpace(insuranceSearchText) == true && insuranceId <= 0)
            {
                visits = _context.Visits.Where(v => v.DoctorId == doctorId).ToList();
            }
            if (string.IsNullOrWhiteSpace(doctorSearchText) == false && doctorId <= 0 && string.IsNullOrWhiteSpace(patientSearchtext) == true && patientId <= 0 && string.IsNullOrWhiteSpace(insuranceSearchText) == true && insuranceId <= 0)
            {
                visits = _context.Visits.Where(v => v.Doctor.DoctorName.Contains(doctorSearchText.Trim())).ToList();
            }
            if (string.IsNullOrWhiteSpace(doctorSearchText) == false && doctorId > 0 && string.IsNullOrWhiteSpace(patientSearchtext) == true && patientId <= 0 && string.IsNullOrWhiteSpace(insuranceSearchText) == true && insuranceId <= 0)
            {
                visits = _context.Visits.Where(v => v.Doctor.DoctorName.Contains(doctorSearchText.Trim()) && v.DoctorId == doctorId).ToList();
            }

            //searching by patient ID and name

            if (string.IsNullOrWhiteSpace(patientSearchtext) == true && patientId > 0 && string.IsNullOrWhiteSpace(doctorSearchText) == true && doctorId <= 0 && string.IsNullOrWhiteSpace(insuranceSearchText) == true && insuranceId <= 0)
            {
                visits = _context.Visits.Where(v => v.PatientId == patientId).ToList();
            }
            if (string.IsNullOrWhiteSpace(patientSearchtext) == false && patientId <= 0 && string.IsNullOrWhiteSpace(doctorSearchText) == true && doctorId <= 0 && string.IsNullOrWhiteSpace(insuranceSearchText) == true && insuranceId <= 0)
            {
                visits = _context.Visits.Where(v => v.Patient.PatientName.Contains(patientSearchtext.Trim())).ToList();
            }
            if (string.IsNullOrWhiteSpace(patientSearchtext) == false && patientId > 0 && string.IsNullOrWhiteSpace(doctorSearchText) == true && doctorId <= 0 && string.IsNullOrWhiteSpace(insuranceSearchText) == true && insuranceId <= 0)
            {
                visits = _context.Visits.Where(v => v.Patient.PatientName.Contains(patientSearchtext.Trim()) && v.PatientId == patientId).ToList();
            }

            //searching by patient ID and name

            if (string.IsNullOrWhiteSpace(doctorSearchText) == true && doctorId <= 0 && string.IsNullOrWhiteSpace(patientSearchtext) == true && patientId <= 0 && string.IsNullOrWhiteSpace(insuranceSearchText) == false && insuranceId <= 0)
            {
                visits = _context.Visits.Where(v => v.InsuranceCompany.InsuranceCompanyName.Contains(insuranceSearchText.Trim())).ToList();
            }
            if (string.IsNullOrWhiteSpace(doctorSearchText) == true && doctorId <= 0 && string.IsNullOrWhiteSpace(patientSearchtext) == true && patientId <= 0 && string.IsNullOrWhiteSpace(insuranceSearchText) == true && insuranceId > 0)
            {
                visits = _context.Visits.Where(v => v.InsuranceCompanyID == insuranceId).ToList();
            }
            if (string.IsNullOrWhiteSpace(doctorSearchText) == true && doctorId <= 0 && string.IsNullOrWhiteSpace(patientSearchtext) == true && patientId <= 0 && string.IsNullOrWhiteSpace(insuranceSearchText) == false && insuranceId > 0)
            {
                visits = _context.Visits.Where(v => v.InsuranceCompany.InsuranceCompanyName.Contains(insuranceSearchText.Trim()) && v.InsuranceCompanyID == insuranceId).ToList();
            }

            return View(visits);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.AllDoctors = _context.Doctors.ToList();
            ViewBag.AllPatients = _context.patients.ToList();
            ViewBag.AllInsuranceCompany = _context.InsuranceCompanies.ToList();
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Visit visit)
        {
            if (ModelState.IsValid == true && visit.DoctorId > 0 && visit.PatientId > 0 )
            {

                Doctor doctor = _context.Doctors.Where(d => d.DocId == visit.DoctorId).FirstOrDefault();
                Patient patient = _context.patients.Where(p => p.PatientId == visit.PatientId).FirstOrDefault();
                InsuranceCompany insuranceCompany = _context.InsuranceCompanies.Where(p => p.InsuranceCompanyId == visit.InsuranceCompanyID).FirstOrDefault();

                visit.VisitingTime = DateTime.Now;
                visit.Prescription = "Need to add Prescription after the visit by pressing edit the visit.";
                visit.VisitcheckUpFree = (doctor.CheckUpFee * (100-insuranceCompany.DiscountPercentage)) / 100;
                if (visit.VisitcheckUpFree == 0)
                {
                    visit.VisitcheckUpFree = doctor.CheckUpFee;
                    _context.Visits.Add(visit);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    _context.Visits.Add(visit);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            else
            {
                ViewBag.AllDoctors = _context.Doctors.ToList();
                ViewBag.AllPatients = _context.patients.ToList();
                ViewBag.AllInsuranceCompany = _context.InsuranceCompanies.ToList();
                return View(visit);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Visit visit = _context.Visits.Find(id);

            if (visit == null)
            {
                ViewBag.AllDoctors = _context.Doctors.ToList();
                ViewBag.AllPatients = _context.patients.ToList();
                ViewBag.AllInsuranceCompany = _context.InsuranceCompanies.ToList();
                return NotFound();
            }
            else
            {
                ViewBag.AllDoctors = _context.Doctors.ToList();
                ViewBag.AllPatients = _context.patients.ToList();
                ViewBag.AllInsuranceCompany = _context.InsuranceCompanies.ToList();
                return View(visit);
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(int id, Visit visit)
        {
            Doctor doctor = _context.Doctors.Where(d => d.DocId == visit.DoctorId).FirstOrDefault();
            Patient patient = _context.patients.Where(p => p.PatientId == visit.PatientId).FirstOrDefault();
            InsuranceCompany insuranceCompany = _context.InsuranceCompanies.Where(i => i.InsuranceCompanyId == visit.InsuranceCompanyID).FirstOrDefault();

            if (id != visit.VisitId)
            {
                return NotFound();
            }
            if (ModelState.IsValid == true)
            {
                visit.VisitingTime = DateTime.Now;
                visit.VisitcheckUpFree = (doctor.CheckUpFee * (100-insuranceCompany.DiscountPercentage)) / 100;
                if (visit.VisitcheckUpFree == 0)
                {
                    visit.VisitcheckUpFree = doctor.CheckUpFee;
                    _context.Visits.Update(visit);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    _context.Visits.Update(visit);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(visit);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            Visit visit = _context.Visits.Include(v => v.Doctor).Include(v => v.Patient).Include(v => v.InsuranceCompany).FirstOrDefault(v=>v.VisitId == id);

            if (visit == null)
            {
                return NotFound();
            }
            else
            {
                return View(visit);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Visit visit = _context.Visits.Include(v => v.Doctor).Include(v => v.Patient).Include(v => v.InsuranceCompany).FirstOrDefault(v => v.VisitId == id);
            if (visit == null)
            {
                return NotFound();
            }
            else
            {
                return View(visit);
            }
        }

        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        [HttpPost]
        public IActionResult DeleteConfirmation(int id)
        {
            Visit visit = _context.Visits.Include(v => v.Doctor).Include(v => v.Patient).Include(v => v.InsuranceCompany).FirstOrDefault(v => v.VisitId == id);

            if (visit == null)
            {
                return NotFound();
            }
            else
            {
                _context.Visits.Remove(visit);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
